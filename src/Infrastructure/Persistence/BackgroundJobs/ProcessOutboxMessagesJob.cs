using Gatherly.Persistence;
using Gatherly.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Quartz;
using StockManagement.Domain.Common;
using StockManagement.Persistence.Database;
using StockManagement.Persistence.Outbox;

namespace StockManagement.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                outboxMessage.Content,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }
            );

            if (domainEvent is null)
            {
                continue;
            }

            AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryForeverAsync();

            PolicyResult result = await policy.ExecuteAndCaptureAsync(
                () => _publisher.Publish(domainEvent, context.CancellationToken)
            );

            outboxMessage.Error = result.FinalException?.Message;
            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}
