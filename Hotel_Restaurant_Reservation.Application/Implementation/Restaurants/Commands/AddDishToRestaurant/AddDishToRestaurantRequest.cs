namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishesToRestaurant;

public class AddDishToRestaurantRequest
{
    public double Price { get; set; }

    public string? Description { get; set; }

    public string? PictureUrl { get; set; }

    public Guid DishId { get; set; }
}
