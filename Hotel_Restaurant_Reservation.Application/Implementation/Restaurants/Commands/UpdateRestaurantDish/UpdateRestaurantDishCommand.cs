using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurantDish;

public class UpdateRestaurantDishCommand : ICommand<Result>
{
    public UpdateRestaurantDishCommand(Guid restaurantId, Guid dishId, UpdateRestaurantDishRequest updateRestaurantDishRequest)
    {
        RestaurantId = restaurantId;
        DishId = dishId;
        UpdateRestaurantDishRequest = updateRestaurantDishRequest;
    }

    public Guid RestaurantId { get; }
    public Guid DishId { get; }
    public UpdateRestaurantDishRequest UpdateRestaurantDishRequest { get; }
}
