using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryHandler : IQueryHandler<GetRoomByIdQuery, Result<RoomResponse>>
{
    private readonly IGenericRepository<Room> _roomRepository;
    private readonly IMapper _mapper;

    public GetRoomByIdQueryHandler(IGenericRepository<Room> roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<Result<RoomResponse>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.Id);
        if (room is null)
        {
            return Result.Failure<RoomResponse>(DomainErrors.Room.NotFound(request.Id));
        }

        var roomResponse = _mapper.Map<RoomResponse>(room);
        return Result.Success(roomResponse);
    }
}