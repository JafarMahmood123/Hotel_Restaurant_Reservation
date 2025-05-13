using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishesToRestaurant;

public class AddDishesToRestaurantCommand : ICommand<Dictionary<Dish, double>>
{
    public AddDishesToRestaurantCommand(Guid restaurantId, Dictionary<Guid, double> dishesIdsWithPrices)
    {
        RestaurantId = restaurantId;
        DishesIdsWithPrices = dishesIdsWithPrices;
    }

    public Guid RestaurantId { get; }
    public Dictionary<Guid, double> DishesIdsWithPrices { get; }
}
