﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockManagement.Domain.Identity;
using StockManagement.Domain.Repositories;
using StockManagement.Infrastructure.Idempotence;
using StockManagement.Persistence.Database;
using StockManagement.Persistence.Interceptors;
using StockManagement.Persistence.Repositories;

namespace StockManagement.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(
        this IServiceCollection serviceCollection,
        IConfiguration configuration
    )
    {
        serviceCollection.AddScoped<ConvertDomainEventsToOutboxMessagesInterceptor>();

        serviceCollection.AddDbContext<ApplicationDbContext>(
            (sm, options) =>
                options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    )
                    .AddInterceptors(
                        sm.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>()
                    )
        );

        serviceCollection
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        serviceCollection.AddAuthorization();
        serviceCollection.AddAuthentication();

        serviceCollection.Decorate(
            typeof(INotificationHandler<>),
            typeof(IdempotentDomainEventHandler<>)
        );

        //serviceCollection.AddScoped(typeof(IDomainEventHandler<>), typeof(IdempotentDomainEventHandler<>));
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
    }
}
