namespace restaurant_server.Entities;

// Class entity to connect Dish and Order with a many to mmany relationship -- used to add dish quantities for an order without having to create multiple dishes with the same name.
public class OrderDish
{
    public int OrderId { get; set; }
    public int DishId { get; set; }

    public Dish Dish { get; set; } = new Dish();
    public Order Order { get; set; } = new Order();

    public int Amount { get; set; } // Amount of Dish in Order.
}
