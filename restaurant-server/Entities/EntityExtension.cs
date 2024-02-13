using restaurant_server.Dtos;

namespace restaurant_server.Entities;

// Entity Extension Dtos.
public static class EntityExtension
{
    public static DishDto AsDto(this Dish dish)
    {
        return new DishDto(
            dish.Id,
            dish.Name,
            dish.Description,
            dish.Image,
            dish.CreatedDate,
            dish.Price,
            dish.Ingredients,
            dish.Orders,
            dish.OrderDishes
        );
    }
    public static IngredientDto AsDto(this Ingredient ingredient)
    {
        return new IngredientDto(
            ingredient.Id,
            ingredient.Name,
            ingredient.Dishes
        );
    }

    public static OrderDto AsDto(this Order order)
    {
        return new OrderDto(
            order.Id,
            order.Created,
            order.Total,
            order.Email,
            order.Dishes,
            order.OrderDishes
        );
    }

    public static OrderDishDto AsDto(this OrderDish orderdish)
    {
        return new OrderDishDto(
            orderdish.OrderId,
            orderdish.DishId,
            orderdish.Dish,
            orderdish.Order,
            orderdish.Amount
        );
    }
}
