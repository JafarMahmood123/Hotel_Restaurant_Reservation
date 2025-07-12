using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommand : ICommand<Result<RoomResponse>>
    {
        public UpdateRoomCommand(Guid id, UpdateRoomRequest updateRoomRequest)
        {
            Id = id;
            UpdateRoomRequest = updateRoomRequest;
        }

        public Guid Id { get; }
        public UpdateRoomRequest UpdateRoomRequest { get; }
    }
}