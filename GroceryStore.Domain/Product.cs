namespace GroceryStore.Domain;

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

    protected Product(
        string name,
        string description,
        DateTime? expiryDate,
        string barcode,
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
}