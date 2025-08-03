using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddMealTypesToRestaurant;

public class AddMealTypesToRestaurantCommand : ICommand<Result<MealTypeResponse>>
{
    public AddMealTypesToRestaurantCommand(Guid restaurantId, Guid mealTypeId)
    {
        RestaurantId = restaurantId;
        MealTypeId = mealTypeId;
    }

    public Guid RestaurantId { get; }
    public Guid MealTypeId { get; }
}