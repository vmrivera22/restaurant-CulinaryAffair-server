using restaurant_server.Dtos;
using restaurant_server.Entities;
using System.Runtime.InteropServices;

namespace restaurant_server.Repositories;

public interface IDishesRepository
{
    // Method to get all dishes in the database.
    Task<List<Dish>> GetAll();

    // Method to get one dish from the database having the ingredients as a list of strings.
    Task<GetDishDto> GetOne(int id);

    // Method to get one dish from the database without modifying the ingredients.
    Task<Dish> GetOneFull(int id);

    // Method to add a dish to the database.
    Task AddOne(CreateDishDto newDish);

    // Method to update a dish to the database.
    Task UpdateOne(UpdateDishDto updatedDish);

    // Method to delete a dish from the database.
    Task DeleteOne(int id);

}
