using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Identity;
using StockManagement.Domain.Repositories;
using StockManagement.Domain.Shared;
using StockManagement.Persistence.Database;

namespace StockManagement.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbContext
            .Set<User>()
            .FirstOrDefaultAsync(member => member.Id == id, cancellationToken);

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default
    ) =>
        await _dbContext
            .Set<User>()
            .FirstOrDefaultAsync(member => member.Email == email, cancellationToken);

    public async Task<bool> IsEmailUniqueAsync(
        string email,
        CancellationToken cancellationToken = default
    ) => !await _dbContext.Set<User>().AnyAsync(member => member.Email == email, cancellationToken);

    public async Task<Result<IdentityResult>> Add(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public void Update(User user) => _dbContext.Set<User>().Update(user);
}
