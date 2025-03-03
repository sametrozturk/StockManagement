﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Identity;
using StockManagement.Domain.Repositories;
using StockManagement.Domain.Shared;
using StockManagement.Persistence.Database;

namespace StockManagement.Persistence.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await dbContext
            .Set<User>()
            .FirstOrDefaultAsync(member => member.Id == id, cancellationToken);

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default
    ) =>
        await dbContext
            .Set<User>()
            .FirstOrDefaultAsync(member => member.Email == email, cancellationToken);

    public async Task<bool> IsEmailUniqueAsync(
        string email,
        CancellationToken cancellationToken = default
    ) => !await dbContext.Set<User>().AnyAsync(member => member.Email == email, cancellationToken);

    public async Task<IdentityResult> Add(User user, string password)
    {
        return await userManager.CreateAsync(user, password);
    }

    public void Update(User user) => dbContext.Set<User>().Update(user);
}
