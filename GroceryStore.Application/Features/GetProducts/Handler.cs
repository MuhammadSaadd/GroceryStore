using FluentResults;
using GroceryStore.Application.Abstractions;
using MediatR;

namespace GroceryStore.Application.Features.GetProducts;

public class Handler(IGroceryRepository repository) : IRequestHandler<Query, Result<List<GetProductResponse>>>
{
    public async Task<Result<List<GetProductResponse>>> Handle(
        Query request,
        CancellationToken cancellationToken)
    {
        var products = await repository.GetAllProductsAsync(cancellationToken);

        return products.Select(p => new GetProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ExpiryDate = p.ExpiryDate,
                Type = p.Type
            })
            .ToList();
    }
}