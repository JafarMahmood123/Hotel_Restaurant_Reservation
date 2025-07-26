using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries.GetCurrencyTypesByRestaurantId
{
    public class GetCurrencyTypesByRestaurantIdQuery : IQuery<Result<IEnumerable<CurrencyTypeResponse>>>
    {
        public GetCurrencyTypesByRestaurantIdQuery(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public Guid RestaurantId { get; }
    }
}