using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries.GetAllRestaurantBookingsByRestaurantId;

public class GetAllRestaurantBookingsByRestaurantIdQuery : IQuery<Result<IEnumerable<RestaurantBookingResponse>>>
{
    public GetAllRestaurantBookingsByRestaurantIdQuery(Guid restaurantId)
    {
        RestaurantId = restaurantId;
    }

    public Guid RestaurantId { get; }
}