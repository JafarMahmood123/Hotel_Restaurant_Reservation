namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddMealTypesToRestaurant;

public class AddMealTypesToRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
