namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurantDish;

public class UpdateRestaurantDishRequest
{
    public double Price { get; set; }

    public string? Description { get; set; }

    public string? PictureUrl { get; set; }
}
