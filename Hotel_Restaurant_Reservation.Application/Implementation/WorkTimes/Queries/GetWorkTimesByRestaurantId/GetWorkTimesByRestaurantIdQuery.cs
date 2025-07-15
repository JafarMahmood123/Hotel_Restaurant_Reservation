using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries.GetWorkTimesByRestaurantId
{
    public class GetWorkTimesByRestaurantIdQuery : IQuery<Result<IEnumerable<WorkTimeResponse>>>
    {
        public GetWorkTimesByRestaurantIdQuery(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public Guid RestaurantId { get; }
    }
}