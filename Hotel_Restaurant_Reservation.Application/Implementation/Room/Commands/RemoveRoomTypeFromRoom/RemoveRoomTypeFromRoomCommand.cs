using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.RemoveRoomTypeFromRoom;

public class RemoveRoomTypeFromRoomCommand : ICommand<Result>
{
    public RemoveRoomTypeFromRoomCommand(Guid roomId)
    {
        RoomId = roomId;
    }

    public Guid RoomId { get; }
}