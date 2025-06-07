using GroceryStore.Application.Abstractions;
using GroceryStore.Domain;
using GroceryStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Infrastructure.Repositories;

public class GroceryRepository(GroceryDbContext db) : IGroceryRepository
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken ct)
    {
        return await db.Products.AsNoTracking().ToListAsync(ct);
    }
}