using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetAllRooms;

public class GetAllRoomsQuery : IQuery<Result<IEnumerable<RoomResponse>>>
{
}