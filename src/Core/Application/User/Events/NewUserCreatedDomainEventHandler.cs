using MassTransit;
using MediatR;
using StockManagement.Domain.Identity.DomainEvents;
using StockManagement.Domain.Repositories;

namespace StockManagement.Application.User.Events;

public sealed class NewUserCreatedDomainEventHandler
    : INotificationHandler<NewUserCreatedDomainEvent>
{
    private readonly IUserRepository _userRepository;
    //private readonly IBus _bus;

    public NewUserCreatedDomainEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(
        NewUserCreatedDomainEvent notification,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId);

        if (user == null)
        {
            return;
        }
    }
}
