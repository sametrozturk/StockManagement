using Microsoft.EntityFrameworkCore;
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

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbContext
            .Set<User>()
            .FirstOrDefaultAsync(member => member.Id == id, cancellationToken);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        await _dbContext
            .Set<User>()
            .FirstOrDefaultAsync(member => member.Email == email, cancellationToken);

    public async Task<bool> IsEmailUniqueAsync(
        string email,
        CancellationToken cancellationToken = default) =>
        !await _dbContext
            .Set<User>()
            .AnyAsync(member => member.Email == email, cancellationToken);

    public void Update(User user) =>
        _dbContext.Set<User>().Update(user);
}
