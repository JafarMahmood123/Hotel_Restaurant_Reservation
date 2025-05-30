using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddMealTypesToRestaurant;

public class AddMealTypesToRestaurantCommand : ICommand<Result<List<MealTypeResponse>>>
{
    public AddMealTypesToRestaurantCommand(Guid restaurantId, AddMealTypesToRestaurantRequest addMealTypeToRestaurantRequest)
    {
        RestaurantId = restaurantId;
        AddMealTypeToRestaurantRequest = addMealTypeToRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public AddMealTypesToRestaurantRequest AddMealTypeToRestaurantRequest { get; }
}