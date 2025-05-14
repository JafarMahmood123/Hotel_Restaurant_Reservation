using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddMealTypesToRestaurant;

public class AddMealTypesToRestaurantCommand : ICommand<IEnumerable<MealType>>
{
    public AddMealTypesToRestaurantCommand(Guid restaurantId, IEnumerable<Guid> mealTypeIds)
    {
        RestaurantId = restaurantId;
        MealTypeIds = mealTypeIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> MealTypeIds { get; }
}
