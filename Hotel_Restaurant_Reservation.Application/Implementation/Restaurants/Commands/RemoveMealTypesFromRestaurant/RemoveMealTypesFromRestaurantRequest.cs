namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveMealTypesFromRestaurant;

public class RemoveMealTypesFromRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
