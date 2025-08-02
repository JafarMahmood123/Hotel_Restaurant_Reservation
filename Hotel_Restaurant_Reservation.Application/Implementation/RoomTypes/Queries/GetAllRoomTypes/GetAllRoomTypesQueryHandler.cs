using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RoomTypes.Queries.GetAllRoomTypes;

public class GetAllRoomTypesQueryHandler : IQueryHandler<GetAllRoomTypesQuery, Result<IEnumerable<RoomTypesResponse>>>
{
    private readonly IGenericRepository<RoomType> _roomTypeRepository;
    private readonly IMapper _mapper;

    public GetAllRoomTypesQueryHandler(IGenericRepository<RoomType> roomTypeRepository, IMapper mapper)
    {
        _roomTypeRepository = roomTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<RoomTypesResponse>>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
    {
        var roomTypes = await _roomTypeRepository.GetAllAsync();

        var result = _mapper.Map<IEnumerable<RoomTypesResponse>>(roomTypes);

        return Result.Success(result);
    }
}
