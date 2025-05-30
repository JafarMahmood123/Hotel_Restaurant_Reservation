namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddTagsToRestaurant;

public class AddTagsToRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
