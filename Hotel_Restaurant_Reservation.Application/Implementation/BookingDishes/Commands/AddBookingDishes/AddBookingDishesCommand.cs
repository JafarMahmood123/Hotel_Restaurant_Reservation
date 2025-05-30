using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.BookingDishDTOs;

namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.AddBookingDishes;

public class AddBookingDishesCommand : ICommand<IEnumerable<BookingDishResponse>>
{
    public AddBookingDishesCommand(Guid bookingId, AddBookingDishesRequest addBookingDishesRequest)
    {
        BookingId = bookingId;
        AddBookingDishesRequest = addBookingDishesRequest;
    }

    public Guid BookingId { get; }
    public AddBookingDishesRequest AddBookingDishesRequest { get; }
}
