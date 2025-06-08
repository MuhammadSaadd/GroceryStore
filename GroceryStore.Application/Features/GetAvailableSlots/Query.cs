using FluentResults;
using MediatR;

namespace GroceryStore.Application.Features.GetAvailableSlots;

public record Query : IRequest<Result<IEnumerable<GetSlotsResponse>>>
{
    public DateTime OrderDate { get; init; }
    public required List<Guid> Ids { get; init; }
}