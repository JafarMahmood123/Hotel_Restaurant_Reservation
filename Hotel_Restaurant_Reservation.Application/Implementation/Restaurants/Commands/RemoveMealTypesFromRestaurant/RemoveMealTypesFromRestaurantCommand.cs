using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveMealTypesFromRestaurant;

public class RemoveMealTypesFromRestaurantCommand : ICommand<Result<List<MealTypeResponse>>>
{
    public RemoveMealTypesFromRestaurantCommand(
        Guid restaurantId,
        RemoveMealTypesFromRestaurantRequest removeMealTypesFromRestaurantRequest)
    {
        RestaurantId = restaurantId;
        RemoveMealTypesFromRestaurantRequest = removeMealTypesFromRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public RemoveMealTypesFromRestaurantRequest RemoveMealTypesFromRestaurantRequest { get; }
}