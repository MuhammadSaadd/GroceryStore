namespace GroceryStore.Application.Features.GetAvailableSlots;

public record GetSlotsResponse
{
    public DateOnly Date { get; init; }
    public TimeOnly Start { get; init; }
    public bool IsGreenDelivery { get; init; }
}