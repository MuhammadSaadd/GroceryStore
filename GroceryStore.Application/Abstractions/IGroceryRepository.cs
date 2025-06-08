namespace GroceryStore.Application.Abstractions;

public interface IGroceryRepository
{
    Task<IEnumerable<Domain.Product>> GetAllProductsAsync(CancellationToken ct);
    Task<IEnumerable<Domain.Product>> GetAllProductsAsync(List<Guid> ids, CancellationToken ct);
    Task<bool> AllProductsExistAsync(List<Guid> ids, CancellationToken ct);
}