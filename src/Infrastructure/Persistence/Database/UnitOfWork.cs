using Newtonsoft.Json;
using StockManagement.Domain.Common;
using StockManagement.Domain.Repositories;
using StockManagement.Persistence.Outbox;

namespace StockManagement.Persistence.Database;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
