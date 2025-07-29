using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetAmenitiesByRoomId;

public class GetAmenitiesByRoomIdQueryHandler : IQueryHandler<GetAmenitiesByRoomIdQuery, Result<IEnumerable<GetAmenitiesByRoomIdResponse>>>
{
    private readonly IGenericRepository<RoomAmenity> _roomAmenitiyRepository;
    private readonly IMapper _mapper;

    public GetAmenitiesByRoomIdQueryHandler(IGenericRepository<RoomAmenity> roomAmenitiyRepository, IMapper mapper)
    {
        _roomAmenitiyRepository = roomAmenitiyRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<GetAmenitiesByRoomIdResponse>>> Handle(GetAmenitiesByRoomIdQuery request, CancellationToken cancellationToken)
    {
        var roomAmenities = await _roomAmenitiyRepository.Where(x => x.RoomId == request.RoomId)
            .Include(x => x.Amenity).ToListAsync();

        var result = new List<GetAmenitiesByRoomIdResponse>();

        foreach (var roomAmenity in roomAmenities)
        {
            result.Add(
                new GetAmenitiesByRoomIdResponse()
                {
                    Id = roomAmenity.AmenityId,
                    Name = roomAmenity.Amenity.Name
                }
            );
        }

        return Result.Success((IEnumerable<GetAmenitiesByRoomIdResponse>)result);
    }
}
