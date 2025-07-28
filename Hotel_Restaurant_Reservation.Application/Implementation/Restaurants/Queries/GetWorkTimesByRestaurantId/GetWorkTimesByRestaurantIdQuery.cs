using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetWorkTimesByRestaurantId;

public class GetWorkTimesByRestaurantIdQuery : IQuery<Result<IEnumerable<GetWorkTimesByRestaurantIdResponse>>>
{
    public GetWorkTimesByRestaurantIdQuery(Guid restaurantId)
    {
        RestaurantId = restaurantId;
    }

    public Guid RestaurantId { get; }
}
