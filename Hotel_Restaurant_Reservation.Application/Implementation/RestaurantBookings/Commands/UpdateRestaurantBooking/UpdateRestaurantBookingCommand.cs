using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.UpdateRestaurantBooking;

public class UpdateRestaurantBookingCommand : ICommand<Result<RestaurantBookingResponse>>
{
    public UpdateRestaurantBookingCommand(Guid id, UpdateRestaurantBookingRequest updateRestaurantBookingRequest)
    {
        Id = id;
        UpdateRestaurantBookingRequest = updateRestaurantBookingRequest;
    }

    public Guid Id { get; }
    public UpdateRestaurantBookingRequest UpdateRestaurantBookingRequest { get; }
}