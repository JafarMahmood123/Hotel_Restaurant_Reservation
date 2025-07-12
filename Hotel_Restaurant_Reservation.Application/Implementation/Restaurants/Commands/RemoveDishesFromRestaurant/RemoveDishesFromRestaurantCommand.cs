using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveDishesFromRestaurant;

public class RemoveDishesFromRestaurantCommand : ICommand<Result<List<DishWithPriceResponse>>>
{
    public RemoveDishesFromRestaurantCommand(
        Guid restaurantId,
        RemoveDishesFromRestaurantRequest request)
    {
        RestaurantId = restaurantId;
        Request = request;
    }

    public Guid RestaurantId { get; }
    public RemoveDishesFromRestaurantRequest Request { get; }
}