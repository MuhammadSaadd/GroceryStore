using System.Text.Json.Serialization;

namespace GroceryStore.Domain.Entities;

public abstract class Product
{
    protected Product()
    {
        Id = Guid.Empty;
        Name = string.Empty;
        Description = string.Empty;
        Price = 0;
        ExpiryDate = null;
    }

    [JsonConstructor]
    protected Product(
        Guid id,
        string name,
        string description,
        decimal price,
        DateTime expiryDate)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        ExpiryDate = expiryDate;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public decimal Price { get; private set; }

    public abstract bool CanBeDeliveredOn(Slot slot, DateOnly orderDate, TimeOnly orderTime);
}