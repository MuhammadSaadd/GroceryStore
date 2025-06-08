using GroceryStore.Domain.Entities;

namespace GroceryStore.Domain.Services;

public class DeliveryPolicyDomainService : IDeliveryPolicyDomainService
{
    public IEnumerable<Slot> FilterAvailableSlots(
        IEnumerable<Slot> allSlots,
        IEnumerable<Product> products,
        DateOnly orderDate,
        TimeOnly orderTime)
    {
        return allSlots.Where(slot =>
            products.All(p => p.CanBeDeliveredOn(slot, orderDate, orderTime)));
    }
}