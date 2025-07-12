using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.UpdateHotelReservation;

public class UpdateHotelReservationCommand : ICommand<Result<HotelReservationResponse>>
{
    public UpdateHotelReservationCommand(Guid id, UpdateHotelReservationRequest updateHotelReservationRequest)
    {
        Id = id;
        UpdateHotelReservationRequest = updateHotelReservationRequest;
    }

    public Guid Id { get; }
    public UpdateHotelReservationRequest UpdateHotelReservationRequest { get; }
}