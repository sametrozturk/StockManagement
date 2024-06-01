using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Repositories;
using StockManagement.Domain.User;

namespace StockManagement.Persistence.Database;

public class ApplicationDbContext : DbContext, IUnitOfWork
{


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

}