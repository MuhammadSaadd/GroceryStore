using GroceryStore.Application.Abstractions;
using GroceryStore.Domain;
using GroceryStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Infrastructure.Repositories;

public class ProductsRepository(GroceryDbContext db) : IProductsRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct)
    {
        return await db.Products.ToListAsync(ct);
    }
}