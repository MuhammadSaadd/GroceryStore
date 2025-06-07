namespace GroceryStore.Application.Abstractions;

public interface IProductsRepository
{
    Task<IEnumerable<Domain.Product>> GetAllAsync(CancellationToken ct);
}