using StockManagement.Domain.Common;
using StockManagement.Domain.Shared;

namespace StockManagement.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string value)
    {
        try
        {
            var email = new System.Net.Mail.MailAddress(value);

            return new Email(value);
        }
        catch (FormatException e)
        {
            return Result.Failure<Email>(new Error($"{e.Source}", $"{e.Message}"));
        }
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
