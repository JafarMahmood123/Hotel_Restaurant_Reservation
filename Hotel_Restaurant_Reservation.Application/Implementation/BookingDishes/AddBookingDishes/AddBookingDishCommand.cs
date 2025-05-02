using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.AddBookingDishes;

public class AddBookingDishCommand : ICommand<BookingDish>
{
    public AddBookingDishCommand(BookingDish bookingDish, RestaurantBooking restaurantBooking)
    {
        BookingDish = bookingDish;
        RestaurantBooking = restaurantBooking;
    }

    public BookingDish BookingDish { get; }
    public RestaurantBooking RestaurantBooking { get; }
}
