namespace StockManagement.Domain.Exceptions;

public sealed class UserEmailDomainExceptions : DomainException
{
    public UserEmailDomainExceptions(string message)
        : base(message) { }
}
