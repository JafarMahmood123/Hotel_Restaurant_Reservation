using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetRoomType;

public class GetRoomTypeQueryHandler : IQueryHandler<GetRoomTypeQuery, Result<string>>
{
    private readonly IGenericRepository<RoomType> _roomTypeRepository;

    public GetRoomTypeQueryHandler(IGenericRepository<RoomType> roomTypeRepository)
    {
        _roomTypeRepository = roomTypeRepository;
    }

    public async Task<Result<string>> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
    {
        var roomType = await _roomTypeRepository.GetByIdAsync(request.RoomTypeId);

        if (roomType == null)
        {
            return Result.Failure<string>(DomainErrors.RoomType.NotFound(request.RoomTypeId));
        }

        return Result.Success(roomType.Description);
    }
}
