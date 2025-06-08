using System.Reflection;
using GroceryStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Infrastructure.Data;

public class GroceryDbContext(DbContextOptions<GroceryDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}