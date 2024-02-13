using restaurant_server.Entities;
using System.ComponentModel.DataAnnotations;

namespace restaurant_server.Dtos;

// Dish Dtos.
public record DishDto(
    int Id,
    string Name,
    string Description,
    string Image,
    DateTime CreatedDate,

    [RegularExpression(@"^[0-9]{0,5}.{1}[0-9]{2}$",
         ErrorMessage = "Invalid Price.")]
    float Price,
    List<Ingredient> Ingredients,
    List<Order> Orders,
    List<OrderDish> OrderDishes
);

public record GetDishDto(
    int Id,
    string Name,
    string Description,
    string Image,
    DateTime CreatedDate,
    float Price,
    List<string> IngredientsString
);

public record CreateDishDto(
    string Name,
    string Description,
    string Image,

    [RegularExpression(@"^[0-9]{0,5}.{1}[0-9]{2}$",
         ErrorMessage = "Invalid Price.")]
    float Price,
    List<string> IngredientsString
);

public record UpdateDishDto(
    int Id,
    string Name,
    string Description,
    string Image,

    [RegularExpression(@"^[0-9]{0,5}.{1}[0-9]{2}$",
         ErrorMessage = "Invalid Price.")]
    float Price,
    List<string> IngredientsString
);


// Ingredient Dtos.
public record IngredientDto(
    int Id,
    string Name,
    List<Dish> Dishes
);

public record CreateIngredientDto(
    string Name
);

// Order Dtos.
public record OrderDto(
    int Id,
    string Created,
    [RegularExpression(@"^[0-9]{0,5}.{1}[0-9]{2}$",
         ErrorMessage = "Invalid Price.")]
    float Total,
    string Email,
    List<Dish> Dishes,
    List<OrderDish> OrderDishes
);

public class OrderItemDto
{
    public string DishName { get; set; }
    public int Quantity { get; set; }
}

public record GetOrderDto(
    int Id,
    string Created,
    [RegularExpression(@"^[0-9]{0,5}.{1}[0-9]{2}$",
         ErrorMessage = "Invalid Price.")]
    float Total,
    string Email,
    List<OrderItemDto> Items 
)
{
    public GetOrderDto() : this(default, default, default, default, default)
    {
    }
}

public record CreateOrderDto(
    float Total,
    List<OrderItemDto> Items,
    string Email
);


// OrderDish Dto.
public record OrderDishDto(
    int OrderId,
    int DishId,
    Dish Dish,
    Order Order,
    int Amount
);