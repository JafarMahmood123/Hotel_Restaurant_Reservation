using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveMealTypesFromRestaurant;

public class RemoveMealTypesFromRestaurantCommand : ICommand<IEnumerable<MealType>>
{
    public RemoveMealTypesFromRestaurantCommand(Guid restaurantId, IEnumerable<Guid> mealTypeIds)
    {
        RestaurantId = restaurantId;
        MealTypeIds = mealTypeIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> MealTypeIds { get; }
}
