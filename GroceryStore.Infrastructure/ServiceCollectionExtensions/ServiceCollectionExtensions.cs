using GroceryStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStore.Infrastructure.ServiceCollectionExtensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GroceryDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("Default")));
    }
}