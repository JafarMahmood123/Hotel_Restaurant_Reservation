using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AssignRoomTypeToRoom;

public class AssignRoomTypeToRoomCommandHandler : ICommandHandler<AssignRoomTypeToRoomCommand, Result>
{
    private readonly IGenericRepository<Room> _roomRepository;
    private readonly IGenericRepository<RoomType> _roomTypeRepository;

    public AssignRoomTypeToRoomCommandHandler(IGenericRepository<Room> roomRepository, IGenericRepository<RoomType> roomTypeRepository)
    {
        _roomRepository = roomRepository;
        _roomTypeRepository = roomTypeRepository;
    }

    public async Task<Result> Handle(AssignRoomTypeToRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.RoomId);
        if (room is null)
        {
            return Result.Failure(DomainErrors.Room.NotFound(request.RoomId));
        }

        var roomType = await _roomTypeRepository.GetByIdAsync(request.RoomTypeId);
        if (roomType is null)
        {
            return Result.Failure(DomainErrors.RoomType.NotFound(request.RoomTypeId));
        }

        room.RoomTypeId = request.RoomTypeId;

        await _roomRepository.UpdateAsync(request.RoomId, room);
        await _roomRepository.SaveChangesAsync();

        return Result.Success();
    }
}