namespace restaurant_server.Entities;

// Class for Ingredient entity.
public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Dish> Dishes { get; set; } = []; // List of Dishes the ingredient is included in -- Many to Many relationship.
}
