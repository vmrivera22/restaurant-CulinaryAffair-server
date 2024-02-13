using Microsoft.EntityFrameworkCore;
using restaurant_server.Entities;

namespace restaurant_server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>() // Crate a many to many relationship between Dishes and Orders while making OrderDish the Entity that connects both.
            .HasMany(e => e.Dishes)
            .WithMany(e => e.Orders)
            .UsingEntity<OrderDish>();
        builder.Entity<Dish>() // Make sure Dishes havev unique name.
            .HasIndex(u => u.Name)
            .IsUnique();
        builder.Entity<Dish>() // Make sure that the dish price has 7 digits max and 2 decimals.
            .Property(p => p.Price)
            .HasColumnType("decimal(7,2)");
    }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set;}

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDish> OrderDishes { get; set; }
}
