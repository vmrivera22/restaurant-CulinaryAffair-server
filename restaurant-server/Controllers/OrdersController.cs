using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_server.Dtos;
using restaurant_server.Entities;
using restaurant_server.Repositories;

namespace restaurant_server.Controllers;

// Controller for orders.
[ApiController]
[Authorize] // Require authorization -- header token -- from Auth0.
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    // Create an instance of the order repository.
    private readonly IOrdersRepository _ordersRepository;
    public OrdersController(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    // Require an email from URI
    [HttpGet("{Email}")]
    public async Task<ActionResult<List<Order>>> GetOrders(string Email )
    {
        var Orders = await _ordersRepository.GetAll(Email); // Get orders related to email and return them.
        return Ok(Orders);
    }

    [HttpPost]
    public async Task<ActionResult> AddOrder(CreateOrderDto newOrder)
    {
        await _ordersRepository.AddOne(newOrder); // Add a new order.
        return Created();
    }
}
