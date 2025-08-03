using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveMealTypesFromRestaurant;

public class RemoveMealTypesFromRestaurantCommand : ICommand<Result<MealTypeResponse>>
{
    public RemoveMealTypesFromRestaurantCommand(Guid restaurantId, Guid mealTypeId)
    {
        RestaurantId = restaurantId;
        MealTypeId = mealTypeId;
    }

    public Guid RestaurantId { get; }
    public Guid MealTypeId { get; }
}