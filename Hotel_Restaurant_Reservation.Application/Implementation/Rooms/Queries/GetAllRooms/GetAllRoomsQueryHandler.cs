using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetAllRooms;

public class GetAllRoomsQueryHandler : IQueryHandler<GetAllRoomsQuery, Result<IEnumerable<RoomResponse>>>
{
    private readonly IGenericRepository<Room> _roomRepository;
    private readonly IMapper _mapper;

    public GetAllRoomsQueryHandler(IGenericRepository<Room> roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<RoomResponse>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomRepository.GetAllAsync();
        var roomResponses = _mapper.Map<IEnumerable<RoomResponse>>(rooms);
        return Result.Success(roomResponses);
    }
}