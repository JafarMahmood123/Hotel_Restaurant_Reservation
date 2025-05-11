namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantDishPrice
{
    // Key Properties
    public Guid Id { get; set; }

    public double Price { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid DishId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual Dish Dish { get; set; }

    public RestaurantDishPrice()
    {

    }
}
