namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveWorkTimesFromRestaurant;

public class RemoveWorkTimesFromRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
