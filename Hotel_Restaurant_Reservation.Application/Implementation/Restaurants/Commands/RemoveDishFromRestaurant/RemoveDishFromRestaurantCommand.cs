using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveDishesFromRestaurant;

public class RemoveDishFromRestaurantCommand : ICommand<Result>
{
    public RemoveDishFromRestaurantCommand(Guid restaurantId, Guid dishId)
    {
        RestaurantId = restaurantId;
        DishId = dishId;
    }

    public Guid RestaurantId { get; }
    public Guid DishId { get; }
}