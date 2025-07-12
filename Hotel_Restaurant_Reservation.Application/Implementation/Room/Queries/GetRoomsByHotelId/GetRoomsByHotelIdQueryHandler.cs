using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetRoomsByHotelId;

public class GetRoomsByHotelIdQueryHandler : IQueryHandler<GetRoomsByHotelIdQuery, Result<IEnumerable<RoomResponse>>>
{
    private readonly IGenericRepository<Room> _roomRepository;
    private readonly IMapper _mapper;

    public GetRoomsByHotelIdQueryHandler(IGenericRepository<Room> roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<RoomResponse>>> Handle(GetRoomsByHotelIdQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomRepository.Where(r => r.HotelId == request.HotelId).ToListAsync(cancellationToken);

        var roomResponses = _mapper.Map<IEnumerable<RoomResponse>>(rooms);

        return Result.Success(roomResponses);
    }
}