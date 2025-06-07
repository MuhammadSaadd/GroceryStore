using GroceryStore.Infrastructure.ServiceCollectionExtensions;

namespace GroceryStore.Gateway;

public static class Extensions
{
    public static void RegisterLayers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
    }

    public static async Task MigrateDatabase(this IServiceProvider serviceProvider, IConfiguration configuration)
    {
        await serviceProvider.Migrate();
    }
}