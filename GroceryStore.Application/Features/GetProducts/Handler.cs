using FluentResults;
using GroceryStore.Application.Abstractions;
using GroceryStore.Domain.Entities;
using MediatR;

namespace GroceryStore.Application.Features.GetProducts;

public class Handler(IGroceryRepository repository) : IRequestHandler<Query, Result<List<GetProductResponse>>>
{
    public async Task<Result<List<GetProductResponse>>> Handle(
        Query request,
        CancellationToken cancellationToken)
    {
        var products = await repository
            .GetAllProductsAsync(cancellationToken);

        return products
            .Select(p => new GetProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ExpiryDate = p.ExpiryDate,
                Type = p switch
                {
                    InStockProduct => "In Stock",
                    FreshFoodProduct => "Fresh Food",
                    ExternalProduct => "External Product",
                    _ => throw new InvalidOperationException($"Unknown product type: {p.GetType()}")
                }
            })
            .ToList();
    }
}