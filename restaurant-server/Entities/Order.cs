namespace restaurant_server.Entities;

// Class for Order entity.
public class Order
{
    public int Id { get; set; }

    public string Created { get; set; } = DateTime.Now.ToString("MM/dd/yy HH:mm tt"); // Date an order was created.
    public float Total { get; set; } // Total price of the order.
    public string Email { get; set; } // Email associated with the order.
    
    public List<Dish> Dishes { get; set; } = []; // Dishes belonging to the order.
    public List<OrderDish> OrderDishes { get; set; } = []; // OrderDish - connect to dishes - used to get quanity of each dish in order.
}
