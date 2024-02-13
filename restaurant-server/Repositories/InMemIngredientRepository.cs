using Microsoft.EntityFrameworkCore;
using restaurant_server.Data;
using restaurant_server.Dtos;
using restaurant_server.Entities;

namespace restaurant_server.Repositories;

// Repository for Ingredients.
public class InMemIngredientsRepository : IIngredientsRepository
{
    // Create an instance of the data context (DbContext).
    private readonly DataContext _data;
    public InMemIngredientsRepository(DataContext dataContext)
    {
        _data = dataContext;
    }

    // Method to get all the ingredients available.
    public async Task<List<Ingredient>> GetAllIng()
    {
        try
        {
            // Get the list of ingredients from the data base.
            var ingredient = await _data.Ingredients.ToListAsync();
            return ingredient;
        }
        catch (Exception ex)
        {
            //Logger.LogError(ex, "Error fetching dishes.");
            throw;
        }
    }

    // Method to get on ingredient based on the name.
    public async Task<Ingredient> GetOneIng(string name)
    {
        try
        {
            Ingredient ingredient = await _data.Ingredients.Where(e => e.Name == name).FirstOrDefaultAsync();
            return ingredient;
        }
        catch (Exception ex)
        {
            //Logger.LogError(ex, "Error fetching dish.")
            throw;
        }
    }

    // Add an ingredient to the database.
    public async Task AddOneIng(CreateIngredientDto newIngredient)
    {
        try
        {
            // Create a new ingredient.
            Ingredient ingredient = new Ingredient { Name = newIngredient.Name };
            _data.Ingredients.Add(ingredient); 
            await _data.SaveChangesAsync();
            return;
        }
        catch (Exception ex)
        {
            //Logger.LoggError(ex, "Error adding new dish.");
            throw;
        }
    }

    // Remove an ingredient form the database.
    public async Task DeleteOneIng(int id) {
        try
        {
            Ingredient i = _data.Ingredients.FirstOrDefault(e => e.Id == id);
            if (i != null)
                return;
            _data.Remove(i);
            await _data.SaveChangesAsync();
            return;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
