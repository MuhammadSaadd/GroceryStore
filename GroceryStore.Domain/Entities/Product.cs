namespace GroceryStore.Domain.Entities;

public abstract class Product(
    Guid id,
    string name,
    string description,
    decimal price,
    DateOnly? expiryDate)
{
    private Product() : this(Guid.Empty, string.Empty, string.Empty, 0, null)
    {
    }

    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public DateOnly? ExpiryDate { get; private set; } = expiryDate;
    public decimal Price { get; private set; } = price;

    public abstract bool CanBeDeliveredOn(Slot slot, DateOnly orderDate, TimeOnly orderTime);
}