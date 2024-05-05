﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockManagement.Application.Common;
using StockManagement.Persistence.Database;

namespace StockManagement.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        serviceCollection.AddScoped(provider =>
        {
            var dbContext = provider.GetService<ApplicationDbContext>();
            if (dbContext == null)
            {
                throw new InvalidOperationException("ApplicationDbContext is null.");
            }
            return dbContext;
        });
    }
}
