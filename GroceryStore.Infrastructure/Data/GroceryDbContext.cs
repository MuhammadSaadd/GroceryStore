using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Infrastructure.Data;

public class GroceryDbContext(DbContextOptions<GroceryDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Product> Products => Set<Domain.Product>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}