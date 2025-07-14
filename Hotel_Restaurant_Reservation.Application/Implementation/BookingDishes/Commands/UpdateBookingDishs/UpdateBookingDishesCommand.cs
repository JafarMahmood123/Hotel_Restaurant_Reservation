using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.UpdateBookingDishes;

public class UpdateBookingDishesCommand : ICommand<Result>
{
    public UpdateBookingDishesCommand(Guid bookingId, UpdateBookingDishesRequest request)
    {
        BookingId = bookingId;
        Request = request;
    }

    public Guid BookingId { get; }
    public UpdateBookingDishesRequest Request { get; }
}