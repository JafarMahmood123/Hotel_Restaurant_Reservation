using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishesToRestaurant;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishToRestaurant;

public class AddDishToRestaurantCommand : ICommand<Result<RestaurantDishResponse>>
{
    public AddDishToRestaurantCommand(
        Guid restaurantId,
        AddDishToRestaurantRequest addDishToRestaurantRequest)
    {
        RestaurantId = restaurantId;
        AddDishToRestaurantRequest = addDishToRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public AddDishToRestaurantRequest AddDishToRestaurantRequest { get; }
}