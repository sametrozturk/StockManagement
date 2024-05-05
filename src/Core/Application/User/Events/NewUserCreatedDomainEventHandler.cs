using MediatR;
using StockManagement.Domain.User.DomainEvents;

namespace StockManagement.Application.User.Events;

public sealed class NewUserCreatedDomainEventHandler
    : INotificationHandler<NewUserCreatedDomainEvent>
{
    public Task Handle(
        NewUserCreatedDomainEvent notification,
        CancellationToken cancellationToken
    ) {
    
       throw new NotImplementedException();
    }
}
