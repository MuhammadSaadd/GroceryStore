namespace GroceryStore.Application.Features.GetProducts;

public record GetProductResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public DateOnly? ExpiryDate { get; init; }
    public decimal Price { get; init; }
    public required string Type { get; init; }
}