using StockManagement.Domain.Common;

namespace StockManagement.Persistence.Database;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IdentityContext _dbContext;

    public UnitOfWork(IdentityContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
