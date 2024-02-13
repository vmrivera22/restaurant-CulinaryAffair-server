using Microsoft.EntityFrameworkCore;
using restaurant_server.Data;
using restaurant_server.Dtos;
using restaurant_server.Entities;

namespace restaurant_server.Repositories;


// Repository for Orders
public class InMemOrderRepository : IOrdersRepository
{
    // Create an instance of the data context (DbContext)
    private readonly DataContext _data;
    public InMemOrderRepository(DataContext data)
    {
        _data = data;
    }

    // Method to get all Orders reltated to the input email.
    public async Task<List<GetOrderDto>> GetAll(string Email)
    {
        // Get the orders related to the input email (Email).
        var orders = await _data.Orders
            .Include(o => o.OrderDishes)
            .ThenInclude(od => od.Dish)
            .Where(o => o.Email == Email)
            .ToListAsync();

        // From the orders from the a GetOrderDto structure to send to the front end. (Easier for the front end to read)
        return orders.Select(order => new GetOrderDto
        {
            Id = order.Id,
            Created = order.Created,
            Total = order.Total,
            Email = order.Email,
            Items = order.OrderDishes.Select(od => new OrderItemDto
            {
                DishName = od.Dish.Name,
                Quantity = od.Amount
            }).ToList()
        }).ToList();
    }

    // Add a new Order.
    public async Task AddOne(CreateOrderDto newOrder)
    {
        try
        {
            // Create a new Order.
            Order order = new Order { Total = newOrder.Total, Email = newOrder.Email };

            // Go throught the string of items included in the order and add the dish to the order.
            foreach(OrderItemDto item in newOrder.Items)
            {
                Dish dbDish = await _data.Dishes
                    .Where(i => i.Name == item.DishName).FirstOrDefaultAsync();
                OrderDish orderdish = new OrderDish { Dish=dbDish, Order = order, Amount = item.Quantity };
                order.OrderDishes.Add(orderdish);
                order.Dishes.Add(dbDish);

            }

            // Add the order to the database and save the changes.
            _data.Orders.Add(order);
            _data.SaveChanges();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
