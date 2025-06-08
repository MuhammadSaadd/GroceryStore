using GroceryStore.Domain.Entities;

namespace GroceryStore.Application.Abstractions;

public interface IGroceryRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken ct);
    Task<IEnumerable<Product>> GetAllProductsAsync(List<Guid> ids, CancellationToken ct);
    Task<bool> AllProductsExistAsync(List<Guid> ids, CancellationToken ct);
}