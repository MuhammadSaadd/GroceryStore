using GroceryStore.Application;
using GroceryStore.Application.Abstractions;
using GroceryStore.Infrastructure.Data;
using GroceryStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStore.Infrastructure.ServiceCollectionExtensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductsRepository, ProductsRepository>();
        
        services.AddApplicationLayer();
        
        services.AddDbContext<GroceryDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("Default")));

    }
}