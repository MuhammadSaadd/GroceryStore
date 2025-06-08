using System.Text.Json;
using GroceryStore.Domain.Entities;

namespace GroceryStore.Infrastructure.Data;

public static class DataSeedingInitialization
{
    public static async Task Seed(GroceryDbContext context)
    {
        await SeedProducts(context);
        await context.SaveChangesAsync();
    }

    private static async Task SeedProducts(GroceryDbContext context)
    {
        if (context.Products.Any()) return;

        Console.WriteLine($"%%%%%%%%%%%%%%%%%%%%{AppContext.BaseDirectory}%%%%%%%%%%%%%%");
        var json = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "Data/products.json"));
        var products = JsonSerializer.Deserialize<List<Product>>(json);

        await context.Products.AddRangeAsync(products ?? []);
    }
}