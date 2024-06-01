using MediatR;

namespace StockManagement.Domain.Common;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
