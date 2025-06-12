using System.Text.Json.Serialization;

namespace GroceryStore.Domain.Entities;

[method: JsonConstructor]
public class InStockProduct(
    Guid id,
    string name,
    string description,
    decimal price,
    DateOnly? expiryDate) : Product(id, name, description, price, expiryDate)
{
    public override bool CanBeDeliveredOn(Slot slot, DateOnly orderDate, TimeOnly orderTime)
    {
        if (orderTime >= new TimeOnly(18, 0))
            return slot.Date > orderDate;
        return true;
    }
}