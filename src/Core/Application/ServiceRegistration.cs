using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace StockManagement.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        var ass = Assembly.GetExecutingAssembly();

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

}
