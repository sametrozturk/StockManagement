using StockManagement.Domain.Common;
using StockManagement.Domain.Shared;
using StockManagement.Domain.User.DomainEvents;
using StockManagement.Domain.ValueObjects;

namespace StockManagement.Domain.User;

public sealed class User : AggregateRoot
{
    public User(
        Guid id,
        Email email,
        string firstName,
        string lastName,
        string phoneNumber,
        short countryCode
    )
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        CountryCode = countryCode;

        RaiseDomainEvent(new NewUserCreatedDomainEvent(id));
    }

    public Guid Id { get; }

    public Email Email { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string PhoneNumber { get; }

    public short CountryCode { get; }

    public bool IsEventWorked { get; set; }
}
