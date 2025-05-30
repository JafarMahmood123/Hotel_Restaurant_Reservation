namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishesToRestaurant;

public class AddDishesWithPricesToRestaurantRequest
{
    public Dictionary<Guid, double> dishIdsWithPrices { get; set; }
}
