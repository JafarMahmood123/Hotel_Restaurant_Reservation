using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RoomTypes.Queries.GetAllRoomTypes;

public class GetAllRoomTypesQuery : IQuery<Result<IEnumerable<RoomTypesResponse>>>
{
}
