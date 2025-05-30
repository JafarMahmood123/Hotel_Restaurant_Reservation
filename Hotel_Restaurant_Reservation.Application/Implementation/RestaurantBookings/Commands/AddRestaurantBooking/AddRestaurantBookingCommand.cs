using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantBookingDTOs;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;

public class AddRestaurantBookingCommand : ICommand<Result<RestaurantBookingResponse>>
{
    public AddRestaurantBookingCommand(AddRestaurantBookingRequest addRestaurantBookingRequest)
    {
        AddRestaurantBookingRequest = addRestaurantBookingRequest;
    }

    public AddRestaurantBookingRequest AddRestaurantBookingRequest { get; }
}
