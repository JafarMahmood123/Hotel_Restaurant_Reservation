using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishesToRestaurant;

public class AddDishesToRestaurantCommand : ICommand<Result<List<DishWithPriceResponse>>>
{
    public AddDishesToRestaurantCommand(
        Guid restaurantId,
        AddDishesWithPricesToRestaurantRequest dishIdsWithPrices)
    {
        RestaurantId = restaurantId;
        DishIdsWithPrices = dishIdsWithPrices;
    }

    public Guid RestaurantId { get; }
    public AddDishesWithPricesToRestaurantRequest DishIdsWithPrices { get; }
}