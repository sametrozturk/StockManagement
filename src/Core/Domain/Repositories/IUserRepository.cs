using Microsoft.AspNetCore.Identity;
using StockManagement.Domain.Identity;
using StockManagement.Domain.Shared;
using StockManagement.Domain.ValueObjects;

namespace StockManagement.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

    Task<IdentityResult> Add(User user, string password);

    void Update(User user);
}
