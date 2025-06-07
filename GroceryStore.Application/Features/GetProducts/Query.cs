using FluentResults;
using MediatR;

namespace GroceryStore.Application.Features.GetProducts;

public record Query : IRequest<Result<List<GetProductResponse>>>;