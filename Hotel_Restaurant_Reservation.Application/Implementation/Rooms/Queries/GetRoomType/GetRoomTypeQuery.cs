using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetRoomType;

public class GetRoomTypeQuery : IQuery<Result<string>>
{
    public GetRoomTypeQuery(Guid roomTypeId)
    {
        RoomTypeId = roomTypeId;
    }

    public Guid RoomTypeId { get; }
}
