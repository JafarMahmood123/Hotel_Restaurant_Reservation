using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCurrencyTypesFromResaturant;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCurrencyTypesFromRestaurant;

public class RemoveCurrencyTypesFromRestaurantCommand : ICommand<Result<List<CurrencyTypeResponse>>>
{
    public RemoveCurrencyTypesFromRestaurantCommand(
        Guid restaurantId,
        RemoveCurrencyTypesFromRestaurantRequest removeCurrencyTypesFromRestaurantRequest)
    {
        RestaurantId = restaurantId;
        RemoveCurrencyTypesFromRestaurantRequest = removeCurrencyTypesFromRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public RemoveCurrencyTypesFromRestaurantRequest RemoveCurrencyTypesFromRestaurantRequest { get; }
}