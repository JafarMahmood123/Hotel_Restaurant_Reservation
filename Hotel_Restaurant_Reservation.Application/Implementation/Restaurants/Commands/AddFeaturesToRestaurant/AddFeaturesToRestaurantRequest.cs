namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddFeaturesToRestaurant;

public class AddFeaturesToRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
