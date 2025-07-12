using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.RemoveRoomFromHotel;

public class RemoveRoomFromHotelCommand : ICommand<Result>
{
    public RemoveRoomFromHotelCommand(Guid hotelId, Guid roomId)
    {
        HotelId = hotelId;
        RoomId = roomId;
    }

    public Guid HotelId { get; }
    public Guid RoomId { get; }
}