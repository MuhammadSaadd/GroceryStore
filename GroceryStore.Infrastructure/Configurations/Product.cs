using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GroceryStore.Infrastructure.Configurations;

public class Product : IEntityTypeConfiguration<Domain.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
    {
        builder.ToTable("Products");
    }
}