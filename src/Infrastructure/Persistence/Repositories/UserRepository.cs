using StockManagement.Domain.Identity;
using StockManagement.Domain.Repositories;
using StockManagement.Domain.ValueObjects;
using StockManagement.Persistence.Database;

namespace StockManagement.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public void Add(User member)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(User member)
    {
        throw new NotImplementedException();
    }
}
