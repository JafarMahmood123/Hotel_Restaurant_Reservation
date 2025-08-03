using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetCurrencyTypesByRestaurantId;

public class GetCurrencyTypesByRestaurantIdQuery : IQuery<Result<IEnumerable<GetCurrencyTypesByRestaurantIdResponse>>>
{
    public GetCurrencyTypesByRestaurantIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
