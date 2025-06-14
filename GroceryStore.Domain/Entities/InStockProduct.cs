namespace GroceryStore.Domain.Entities;

public class InStockProduct(
    Guid id,
    string name,
    string description,
    decimal price,
    DateOnly? expiryDate) : Product(id, name, description, price, expiryDate)
{
    public override bool CanBeDeliveredOn(Slot slot, DateOnly orderDate, TimeOnly orderTime)
    {
        if (orderTime >= new TimeOnly(Constants.InStockSameDayDeliveryCutoff, 0))
            return slot.Date > orderDate;
        return true;
    }
}