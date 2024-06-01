﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockManagement.Domain.Repositories;
using StockManagement.Persistence.Database;

namespace StockManagement.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(
        this IServiceCollection serviceCollection,
        IConfiguration configuration
    )
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            )
        );

        serviceCollection.AddAuthorization();
        serviceCollection.AddAuthentication();

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
