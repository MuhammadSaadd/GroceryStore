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

        var json = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "Data/products.json"));
        var productDtos = JsonSerializer.Deserialize<List<ProductDto>>(json);

        var products = productDtos.Select(dto => (Product?)(dto.Type switch
            {
                (int)ProductType.InStock => new InStockProduct(dto.Id, dto.Name, dto.Description, dto.Price,
                    dto.ExpiryDate),
                (int)ProductType.External => new ExternalProduct(dto.Id, dto.Name, dto.Description, dto.Price,
                    dto.ExpiryDate),
                (int)ProductType.FreshFood => new FreshFoodProduct(dto.Id, dto.Name, dto.Description, dto.Price,
                    dto.ExpiryDate),
                _ => null
            }))
            .OfType<Product>()
            .ToList();

        await context.Products.AddRangeAsync(products);
    }
}

public class ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateOnly? ExpiryDate { get; init; }
    public decimal Price { get; init; }
    public int Type { get; init; }
}