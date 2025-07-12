using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCurrencyTypesToRestaurant;

public class AddCurrencyTypesToRestaurantCommand : ICommand<Result<List<CurrencyTypeResponse>>>
{
    public AddCurrencyTypesToRestaurantCommand(
        Guid restaurantId,
        AddCurrencyTypeToRestaurantRequest addCurrencyTypeToRestaurantRequest)
    {
        RestaurantId = restaurantId;
        AddCurrencyTypeToRestaurantRequest = addCurrencyTypeToRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public AddCurrencyTypeToRestaurantRequest AddCurrencyTypeToRestaurantRequest { get; }
}