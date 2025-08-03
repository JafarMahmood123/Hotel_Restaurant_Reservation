namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantDish
{
    // Key Properties
    public Guid Id { get; set; }

    public double Price { get; set; }

    public string? Description { get; set; }

    public string? PictureUrl { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid DishId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual Dish Dish { get; set; }

    public RestaurantDish()
    {

    }
}
