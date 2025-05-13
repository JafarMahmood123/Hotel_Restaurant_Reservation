using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCurrencyTypesFromResaturant;

public class RemoveCurrencyTypesFromResaturantCommand : ICommand<IEnumerable<CurrencyType>>
{
    public RemoveCurrencyTypesFromResaturantCommand(Guid restaurantId, IEnumerable<Guid> currencyTypeIds)
    {
        RestaurantId = restaurantId;
        CurrencyTypeIds = currencyTypeIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> CurrencyTypeIds { get; }
}
