using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using restaurant_server.Data;
using restaurant_server.Dtos;
using restaurant_server.Entities;
using System;

namespace restaurant_server.Repositories;

// Repository for Dishes.
public class InMemDishesRepository : IDishesRepository
{
    // Create an instance of the data context (DbContext).
    private readonly DataContext _data;
    public InMemDishesRepository(DataContext dataContext)
    {
        _data = dataContext;
    }

    // Method to get all dishes from the database.
    public async Task<List<Dish>> GetAll()
    {
        try
        {
            var dish = await _data.Dishes
                .ToListAsync();
            return dish;
        }
        catch (Exception ex)
        {
            //Logger.LogError(ex, "Error fetching dishes.");
            throw;
        }
    }

    // Method to get one dish from the database -- based on id.
    public async Task<GetDishDto> GetOne(int id)
    {
        try
        {
            // Get the dish.
            Dish dish = await _data.Dishes.FindAsync(id);

            // Get the ingredients of the dish.
            IQueryable<Ingredient> ingred = from d in _data.Dishes
                                      where d.Id == id
                                      from ing in d.Ingredients
                                      select ing;

            // Add the ingredients of the dish to a string array.
            List<string> ingredients = [];
            foreach(Ingredient I in ingred)
            {
                Console.WriteLine(I.Name);
                ingredients.Add(I.Name);
            }

            // Return the dish with the ingredients as a list of strings.
            GetDishDto dishDto = new GetDishDto( dish.Id, dish.Name, dish.Description, dish.Image, dish.CreatedDate, dish.Price, ingredients);
            return dishDto;
        }
        catch (Exception ex)
        {
            //Logger.LogError(ex, "Error fetching dish.")
            throw;
        }
    }

    // Method to get a dish without altering the ingredients.
    public async Task<Dish> GetOneFull(int id)
    {
        try
        {
            Dish dish = await _data.Dishes.FindAsync(id);
            return dish;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    // Method to add a Dish to the database.
    public async Task AddOne(CreateDishDto newDish)
    {
        try
        {
            // Create a new dish.
            Dish dish = new Dish { Name = newDish.Name, Description = newDish.Description, Image = newDish.Image, Price = newDish.Price };
            
            // Create a list of ingredients based on the input list of ingredients.
            List<Ingredient> ing = [];
            foreach (string ingredientName in newDish.IngredientsString)
            {
                // Find the ingredient in the database.
                Ingredient ingredient = await _data.Ingredients
                    .FirstOrDefaultAsync(i => i.Name == ingredientName);

                // If the ingredient is not in the database then create a new ingredient with that name.
                if (ingredient == null)
                {
                    ingredient = new Ingredient { Name = ingredientName };
                    _data.Ingredients.Add(ingredient);
                }
                ing.Add(ingredient);
                ingredient.Dishes.Add(dish);
            }

            // Add the ingredients to the dish.
            dish.Ingredients = ing;

            _data.Dishes.Add(dish);
            await _data.SaveChangesAsync();
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
    
    // Method to update a dish.
    public async Task UpdateOne(UpdateDishDto updatedDish)
    {
        try
        {
            // Find the dish that has a matching Id.
            Dish dbDish = _data.Dishes.Include(x => x.Ingredients).FirstOrDefault(x => x.Id == updatedDish.Id);
            
            // Remove all the ingredients that were in the dish.
            dbDish.Ingredients.Clear();

            _data.Update(dbDish);
            await _data.SaveChangesAsync();

            // Add the ingredients from the updated dish to the dish in the data base.
            foreach (string ingredientName in updatedDish.IngredientsString)
            {
                Ingredient ingredient = await _data.Ingredients
                    .FirstOrDefaultAsync(i => i.Name == ingredientName);

                if (ingredient == null)
                {
                    ingredient = new Ingredient { Name = ingredientName };
                    _data.Ingredients.Add(ingredient);
                }
                dbDish.Ingredients.Add(ingredient);
            }

            // Update the dish name, price, and image.
            dbDish.Name = updatedDish.Name;
            dbDish.Description = updatedDish.Description;
            dbDish.Price = updatedDish.Price;
            dbDish.Image = updatedDish.Image;
            _data.Update(dbDish);
            await _data.SaveChangesAsync();
        }
        catch {
            //Logger.LoggError(ex, "Error updating dish.");
            throw;
        }
    }

    // Method to delete a dish.
    public async Task DeleteOne(int id)
    {
        try
        {
            var dish = await _data.Dishes.FindAsync(id);
            _data.Dishes.Remove(dish);
            await _data.SaveChangesAsync();
            return;
        }
        catch
        {
            //Logger.LoggError(ex, "Error deleting dish.");
            throw;
        }
    }

}
