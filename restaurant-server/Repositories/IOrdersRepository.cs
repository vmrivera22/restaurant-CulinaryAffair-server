using restaurant_server.Dtos;
using restaurant_server.Entities;

namespace restaurant_server.Repositories;

public interface IOrdersRepository
{
    // Method that get Orders related to input email.
    Task<List<GetOrderDto>> GetAll(string Email);

    // Method to add a new Order.
    Task AddOne(CreateOrderDto newOrder);
}
