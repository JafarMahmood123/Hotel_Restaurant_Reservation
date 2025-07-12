using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetRoomById;

public class GetRoomByIdQuery : IQuery<Result<RoomResponse>>
{
    public GetRoomByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}