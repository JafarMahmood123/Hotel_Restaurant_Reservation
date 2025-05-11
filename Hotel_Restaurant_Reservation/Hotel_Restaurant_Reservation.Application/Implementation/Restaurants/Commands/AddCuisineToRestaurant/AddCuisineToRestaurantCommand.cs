using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;

public class AddCuisineToRestaurantCommand : ICommand<Cuisine>
{
    public AddCuisineToRestaurantCommand(Guid restaurantId, Guid cuisineId)
    {
        RestaurantId = restaurantId;
        CuisineId = cuisineId;
    }

    public Guid RestaurantId { get; }
    public Guid CuisineId { get; }
}
