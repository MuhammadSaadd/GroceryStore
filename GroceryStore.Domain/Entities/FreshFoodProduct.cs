namespace GroceryStore.Domain.Entities;

public class FreshFoodProduct(
    Guid id,
    string name,
    string description,
    decimal price,
    DateOnly? expiryDate) : Product(id, name, description, price, expiryDate)
{
    public override bool CanBeDeliveredOn(Slot slot, DateOnly orderDate, TimeOnly orderTime)
    {
        if (orderTime >= new TimeOnly(Constants.FreshFoodSameDayDeliveryCutoff, 0)) // 1
            return slot.Date > orderDate;
        return true;
    }
}