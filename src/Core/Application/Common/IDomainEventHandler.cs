using MediatR;
using StockManagement.Domain.Common;

namespace StockManagement.Application.Common;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
