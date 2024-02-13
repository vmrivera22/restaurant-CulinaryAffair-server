using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant_server.Data;
using restaurant_server.Dtos;
using restaurant_server.Entities;
using restaurant_server.Repositories;

namespace restaurant_server.Controllers;

// Controller for Dishes
[Route("api/[controller]")]
[ApiController]
public class DishesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Dish>>> GetAllDishes(IDishesRepository repositroy)
    {
        var dish = await repositroy.GetAll(); // Get all the dishes in the database.
        return Ok(dish);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetDishDto>> GetDish(IDishesRepository repositroy, int id)
    {
        var dish = await repositroy.GetOne(id); // Get a dish from the database.
        return Ok(dish);
    }

    [HttpPost]
    public async Task<ActionResult> AddDish(IDishesRepository repositroy, CreateDishDto newDish)
    {
        await repositroy.AddOne(newDish); // Add a dish to the database.
        return Created();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateDish(IDishesRepository repositroy, UpdateDishDto updatedDish)
    {
        var dish = await repositroy.GetOneFull(updatedDish.Id); // Make sure that the dish exists in the database.
        if (dish is null)
        {
            return BadRequest();
        }
        await repositroy.UpdateOne(updatedDish); // Update the dish in the databse.
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteDish(IDishesRepository repositroy, int id)
    {
        await repositroy.DeleteOne(id); // Delete a dish from the database.
        return NoContent();
    }
}
