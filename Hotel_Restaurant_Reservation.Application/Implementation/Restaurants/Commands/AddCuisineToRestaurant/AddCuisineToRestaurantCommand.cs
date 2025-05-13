using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;

public class AddCuisineToRestaurantCommand : ICommand<IEnumerable<Cuisine>>
{
    public AddCuisineToRestaurantCommand(Guid restaurantId, IEnumerable<Guid> cuisineIds)
    {
        RestaurantId = restaurantId;
        CuisineIds = cuisineIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> CuisineIds { get; }

}
