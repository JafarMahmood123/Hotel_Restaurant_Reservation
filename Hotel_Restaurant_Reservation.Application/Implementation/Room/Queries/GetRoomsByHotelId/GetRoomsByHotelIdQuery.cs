using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetRoomsByHotelId;

public class GetRoomsByHotelIdQuery : IQuery<Result<IEnumerable<RoomResponse>>>
{
    public GetRoomsByHotelIdQuery(Guid hotelId)
    {
        HotelId = hotelId;
    }

    public Guid HotelId { get; }
}