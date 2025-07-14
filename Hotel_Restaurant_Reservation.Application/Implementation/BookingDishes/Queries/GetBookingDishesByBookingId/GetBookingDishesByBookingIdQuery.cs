using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Queries.GetBookingDishesByBookingId;

public class GetBookingDishesByBookingIdQuery : IQuery<Result<IEnumerable<BookingDishResponse>>>
{
    public GetBookingDishesByBookingIdQuery(Guid bookingId)
    {
        BookingId = bookingId;
    }

    public Guid BookingId { get; }
}