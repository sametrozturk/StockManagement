namespace StockManagement.Domain.Common;

public interface IAggregateRoot
{
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents();

    public void ClearDomainEvents();

    public void RaiseDomainEvent(IDomainEvent domainEvent);
}
