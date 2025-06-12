namespace GroceryStore.Domain.Entities;

public class ExternalProduct(
    Guid id,
    string name,
    string description,
    decimal price,
    DateOnly? expiryDate) : Product(id, name, description, price, expiryDate)
{
    public override bool CanBeDeliveredOn(Slot slot, DateOnly orderDate, TimeOnly orderTime)
    {
        var minDate = orderDate.AddDays(3);
        return slot.Date >= minDate &&
               slot.Date.DayOfWeek is >= DayOfWeek.Tuesday and <= DayOfWeek.Friday;
    }
}