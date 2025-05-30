namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveDishesFromRestaurant;

public class RemoveDishesFromRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
