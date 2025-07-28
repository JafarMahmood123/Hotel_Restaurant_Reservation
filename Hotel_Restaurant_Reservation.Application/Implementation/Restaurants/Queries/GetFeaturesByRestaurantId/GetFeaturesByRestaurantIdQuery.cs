using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetFeaturesByRestaurantId;

public class GetFeaturesByRestaurantIdQuery : IQuery<Result<IEnumerable<GetFeaturesByRestaurantIdResponse>>>
{
    public GetFeaturesByRestaurantIdQuery(Guid restaurantId)
    {
        RestaurantId = restaurantId;
    }

    public Guid RestaurantId { get; }
}
