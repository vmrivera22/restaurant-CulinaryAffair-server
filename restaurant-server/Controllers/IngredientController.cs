using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_server.Dtos;
using restaurant_server.Entities;
using restaurant_server.Repositories;

namespace restaurant_server.Controllers;

// Controller for ingredients.
[Route("api/[controller]")]
[ApiController]
public class IngredientController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Ingredient>>> GetAllIngredients(IIngredientsRepository repository)
    {
        var ingredients = await repository.GetAllIng(); // Get all ingredients in the database.
        return Ok(ingredients);
    }

    // Require an ingredient name in the URI
    [HttpGet("{name}")]
    public async Task<ActionResult<Ingredient>> GetIngredient(IIngredientsRepository repositroy, string name)
    {
        var ingredient = await repositroy.GetOneIng(name); // Get the ingredient.
        return Ok(ingredient);
    }

    [HttpPost]
    public async Task<ActionResult> AddIngredient(IIngredientsRepository repositroy, CreateIngredientDto newIngredient)
    {
        await repositroy.AddOneIng(newIngredient); // Add an ingredient.
        return Created();
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveIngredient(IIngredientsRepository repository, int id)
    {
        await repository.DeleteOneIng(id);
        return NoContent();
    }
}
