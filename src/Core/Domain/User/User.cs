using Microsoft.AspNetCore.Identity;
using StockManagement.Domain.Common;
using StockManagement.Domain.ValueObjects;

namespace StockManagement.Domain.User;

public sealed class User : AggregateRoot
{
    public User(Guid id) : base(id)
    {
    }
}
