using GroceryStore.Domain;
using Microsoft.EntityFrameworkCore;
using GroceryStore.Infrastructure.Data;
using GroceryStore.Application.Abstractions;

namespace GroceryStore.Infrastructure.Repositories;

public class GroceryRepository(GroceryDbContext db) : IGroceryRepository
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken ct)
    {
        return await db.Products.AsNoTracking().ToListAsync(ct);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(List<Guid> ids, CancellationToken ct)
    {
        var set = ids.ToHashSet();

        var products = await db.Products
            .AsNoTracking()
            .Where(p => set.Contains(p.Id))
            .ToListAsync(ct);

        return products;
    }

    public async Task<bool> AllProductsExistAsync(List<Guid> ids, CancellationToken ct)
    {
        var set = ids.ToHashSet();

        return await db.Products
            .AsNoTracking()
            .AnyAsync(p => set.Contains(p.Id), cancellationToken: ct);
    }
}