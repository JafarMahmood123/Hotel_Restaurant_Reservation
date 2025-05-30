using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries.GetRestaurantBookingsByCustomerId;

public class GetRestaurantBookingsByCustomerIdQuery : IQuery<Result<IEnumerable<RestaurantBookingResponse>>>
{
    public GetRestaurantBookingsByCustomerIdQuery(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; }
}
