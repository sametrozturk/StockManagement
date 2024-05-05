using StockManagement.Domain.Common;

namespace StockManagement.Domain.User.DomainEvents;

public sealed record NewUserCreatedDomainEvent(Guid UserId) : IDomainEvent
{
}
