using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.AddHotelReservation;

public class AddHotelReservationCommand : ICommand<Result<HotelReservationResponse>>
{
    public AddHotelReservationCommand(AddHotelReservationRequest addHotelReservationRequest)
    {
        AddHotelReservationRequest = addHotelReservationRequest;
    }

    public AddHotelReservationRequest AddHotelReservationRequest { get; }
}