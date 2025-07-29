using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetRoomsByHotelId;

public class GetRoomsByHotelIdQueryHandler : IQueryHandler<GetRoomsByHotelIdQuery, Result<IEnumerable<GetRoomsByHotelIdResponse>>>
{
    private readonly IGenericRepository<Room> _roomRepository;
    private readonly IMapper _mapper;

    public GetRoomsByHotelIdQueryHandler(IGenericRepository<Room> roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<GetRoomsByHotelIdResponse>>> Handle(GetRoomsByHotelIdQuery request, CancellationToken cancellationToken)
    {
        var hotelRooms = await _roomRepository.Where(x => x.HotelId == request.HotelId)
            .Include(x => x.RoomType)
            .ToListAsync();

        var result = _mapper.Map<List<GetRoomsByHotelIdResponse>>(hotelRooms);

        return Result.Success((IEnumerable<GetRoomsByHotelIdResponse>)result);
    }
}
