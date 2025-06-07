using GroceryStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStore.Infrastructure.ServiceCollectionExtensions;

public static class ServiceProviderExtensions
{
    public static async Task MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GroceryDbContext>();
        await context.Database.MigrateAsync();
        await DataSeedingInitialization.Seed(context);
    }
}