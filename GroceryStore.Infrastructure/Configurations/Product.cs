using GroceryStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GroceryStore.Infrastructure.Configurations;

public class Product : IEntityTypeConfiguration<Domain.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Product> builer)
    {
        builer.ToTable("Products");

        builer
            .HasDiscriminator<int>("Type")
            .HasValue<InStockProduct>((int)ProductType.InStock)
            .HasValue<ExternalProduct>((int)ProductType.External)
            .HasValue<FreshFoodProduct>((int)ProductType.FreshFood);
    }
}