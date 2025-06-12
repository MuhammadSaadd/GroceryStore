namespace GroceryStore.Domain.Entities;

public abstract class Product
{
    private Product()
    {
        Id = Guid.Empty;
        Name = string.Empty;
        Description = string.Empty;
        Price = 0;
        ExpiryDate = null;
    }
    
    protected Product(
        Guid id,
        string name,
        string description,
        decimal price,
        DateOnly? expiryDate)
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
    public DateOnly? ExpiryDate { get; private set; }
    public decimal Price { get; private set; }
    
    public abstract bool CanBeDeliveredOn(Slot slot, DateOnly orderDate, TimeOnly orderTime);
}