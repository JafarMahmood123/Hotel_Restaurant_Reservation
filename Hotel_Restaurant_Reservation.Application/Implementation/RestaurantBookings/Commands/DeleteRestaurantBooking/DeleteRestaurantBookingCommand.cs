using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.DeleteRestaurantBooking;

public class DeleteRestaurantBookingCommand : ICommand<Result>
{
    public DeleteRestaurantBookingCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}