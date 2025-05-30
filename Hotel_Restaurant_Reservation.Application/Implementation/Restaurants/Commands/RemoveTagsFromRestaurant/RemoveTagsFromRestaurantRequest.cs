namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveTagsFromRestaurant;

public class RemoveTagsFromRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
