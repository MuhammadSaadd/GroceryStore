using GroceryStore.Domain.Entities;

namespace GroceryStore.Domain.Services;

public interface IDeliverableDomainService 
{
    public IEnumerable<Slot> FilterAvailableSlots(
        IEnumerable<Slot> allSlots,
        IEnumerable<Product> products,
        DateOnly orderDate,
        TimeOnly orderTime);
}