using GroceryStore.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStore.Domain;

public static class ServiceCollectionExtensions
{
    public static void AddDomainLayer(this IServiceCollection services)
    {
        services.AddScoped<IDeliveryPolicyDomainService, DeliveryPolicyDomainService>();
    }
}