using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.UpdateHotel;

public class UpdateHotelCommand : ICommand<Result<HotelResponse>>
{
    public UpdateHotelCommand(Guid id, UpdateHotelRequest updateHotelRequest)
    {
        Id = id;
        UpdateHotelRequest = updateHotelRequest;
    }

    public Guid Id { get; }
    public UpdateHotelRequest UpdateHotelRequest { get; }
}