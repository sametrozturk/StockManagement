namespace StockManagement.Domain.Common;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
