using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.RemoveRoomTypeFromRoom;

public class RemoveRoomTypeFromRoomCommandHandler : ICommandHandler<RemoveRoomTypeFromRoomCommand, Result>
{
    private readonly IGenericRepository<Room> _roomRepository;

    public RemoveRoomTypeFromRoomCommandHandler(IGenericRepository<Room> roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result> Handle(RemoveRoomTypeFromRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.RoomId);
        if (room is null)
        {
            return Result.Failure(DomainErrors.Room.NotFound(request.RoomId));
        }

        room.RoomTypeId = Guid.Empty;

        await _roomRepository.UpdateAsync(request.RoomId, room);
        await _roomRepository.SaveChangesAsync();

        return Result.Success();
    }
}