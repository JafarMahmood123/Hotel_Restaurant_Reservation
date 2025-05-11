using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;

public class AddRestaurantBookingCommand : ICommand<RestaurantBooking?>
{
    public AddRestaurantBookingCommand(RestaurantBooking restaurantBooking)
    {
        RestaurantBooking = restaurantBooking;
    }

    public RestaurantBooking RestaurantBooking { get; }
}
