using System.Text.Json.Serialization;

namespace GroceryStore.Domain.Entities;

public class Product
{
    #region Constructor

    private Product()
    {
        Id = Guid.Empty;
        Name = string.Empty;
        Description = string.Empty;
        ExpiryDate = null;
        Price = 0m;
        Type = default;
    }

    [JsonConstructor]
    protected Product(
        string name,
        string description,
        DateTime? expiryDate,
        decimal price,
        ProductType type) : this()
    {
        Name = name;
        Description = description;
        ExpiryDate = expiryDate;
        Price = price;
        Type = type;
    }

    #endregion Constructor

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public decimal Price { get; private set; }
    public ProductType Type { get; private set; }
    
    public bool CanBeDeliveredOn(Slot slot, DateOnly orderDate, TimeOnly orderTime)
    {
        return Type switch
        {
            ProductType.External => CanExternalBeDelivered(slot, orderDate),
            ProductType.InStock => CanInStockBeDelivered(slot, orderDate, orderTime),
            ProductType.FreshFood => CanFreshFoodBeDelivered(slot, orderDate, orderTime),
            _ => true
        };
    }

    private static bool CanExternalBeDelivered(Slot slot, DateOnly orderDate)
    {
        var minDate = orderDate.AddDays(3);
        return slot.Date >= minDate &&
               slot.Date.DayOfWeek is >= DayOfWeek.Tuesday and <= DayOfWeek.Friday;
    }

    private static bool CanInStockBeDelivered(Slot slot, DateOnly orderDate, TimeOnly orderTime)
    {
        if (orderTime >= new TimeOnly(18, 0))
            return slot.Date > orderDate;
        return true;
    }

    private static bool CanFreshFoodBeDelivered(Slot slot, DateOnly orderDate, TimeOnly orderTime)
    {
        if (orderTime >= new TimeOnly(12, 0))
            return slot.Date > orderDate;
        return true;
    }
}