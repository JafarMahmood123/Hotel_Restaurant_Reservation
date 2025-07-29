using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AssignRoomTypeToRoom;

public class AssignRoomTypeToRoomCommand : ICommand<Result>
{
    public AssignRoomTypeToRoomCommand(Guid roomId, Guid roomTypeId)
    {
        RoomId = roomId;
        RoomTypeId = roomTypeId;
    }

    public Guid RoomId { get; }
    public Guid RoomTypeId { get; }
}