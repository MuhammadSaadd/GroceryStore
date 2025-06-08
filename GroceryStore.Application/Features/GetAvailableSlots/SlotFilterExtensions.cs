using GroceryStore.Domain;

namespace GroceryStore.Application.Features.GetAvailableSlots;

public static class SlotFilterExtensions
{
    public static IQueryable<Slot> ApplyFilter(
        this IQueryable<Slot> slots,
        List<Product> products,
        DateOnly orderDate,
        TimeOnly orderTime,
        Func<IQueryable<Slot>, List<Product>, DateOnly, TimeOnly, IQueryable<Slot>> filter)
    {
        return filter(slots, products, orderDate, orderTime);
    }
}