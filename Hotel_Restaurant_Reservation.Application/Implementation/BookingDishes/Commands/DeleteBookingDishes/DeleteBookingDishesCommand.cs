using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.DeleteBookingDishes;

public class DeleteBookingDishesCommand : ICommand<Result>
{
    public DeleteBookingDishesCommand(Guid bookingId, DeleteBookingDishesRequest request)
    {
        BookingId = bookingId;
        Request = request;
    }

    public Guid BookingId { get; }
    public DeleteBookingDishesRequest Request { get; }
}