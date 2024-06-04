
using StockManagement.Domain.Common;

namespace StockManagement.Domain.Identity.DomainEvents;

public sealed record NewUserCreatedDomainEvent(Guid UserId):  DomainEvent(UserId);