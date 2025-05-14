using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveDishesFromRestaurant;

public class RemoveDishesFromRestaurantCommand : ICommand<Dictionary<Dish, double>>
{
    public RemoveDishesFromRestaurantCommand(Guid restaurantId, IEnumerable<Guid> dishesIds)
    {
        RestaurantId = restaurantId;
        DishesIds = dishesIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> DishesIds { get; }
}
