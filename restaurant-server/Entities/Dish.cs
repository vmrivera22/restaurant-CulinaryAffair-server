using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant_server.Entities;

// Class for Dish entity.
public class Dish
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string Image { get; set;} = string.Empty;

    public float Price { get; set; }

    public List<Ingredient> Ingredients { get; set; } = []; // Ingredients included in the dish -- creates a many to many relationship -- connection table handled by EF.

    public List<Order> Orders { get; set; } = []; // Orders that include the dish -- creates a many to many relationship.
    public List<OrderDish> OrderDishes { get; set; } = []; // Entity that handles connection between dishes and orders.
}
