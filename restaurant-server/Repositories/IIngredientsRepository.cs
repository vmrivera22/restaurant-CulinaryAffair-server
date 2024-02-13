using restaurant_server.Dtos;
using restaurant_server.Entities;

namespace restaurant_server.Repositories;

public interface IIngredientsRepository
{
    // Method to get all ingredients in the database.
    Task<List<Ingredient>> GetAllIng();

    // Method to get one ingredient from the database.
    Task<Ingredient> GetOneIng(string name);

    // Method to add an ingredient to the database.
    Task AddOneIng(CreateIngredientDto newIngredient);

    Task DeleteOneIng(int id);
}
