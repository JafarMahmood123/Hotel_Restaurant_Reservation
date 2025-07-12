using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.DeleteHotelReservation;

public class DeleteHotelReservationCommand : ICommand<Result>
{
    public DeleteHotelReservationCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}