namespace GroceryStore.Application.Abstractions;

public interface IGroceryRepository
{
    Task<IEnumerable<Domain.Product>> GetAllProductsAsync(CancellationToken ct);
}