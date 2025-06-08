using System.Reflection;
using GroceryStore.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStore.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddDomainLayer();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}