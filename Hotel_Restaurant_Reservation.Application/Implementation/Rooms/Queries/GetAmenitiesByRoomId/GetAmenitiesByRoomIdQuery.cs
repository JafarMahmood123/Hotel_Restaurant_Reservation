using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetAmenitiesByRoomId;

public class GetAmenitiesByRoomIdQuery : IQuery<Result<IEnumerable<GetAmenitiesByRoomIdResponse>>>
{
    public GetAmenitiesByRoomIdQuery(Guid roomId)
    {
        RoomId = roomId;
    }

    public Guid RoomId { get; }
}
