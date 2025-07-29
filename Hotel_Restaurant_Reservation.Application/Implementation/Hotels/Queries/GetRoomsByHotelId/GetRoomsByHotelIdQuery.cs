using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetRoomsByHotelId;

public class GetRoomsByHotelIdQuery : IQuery<Result<IEnumerable<GetRoomsByHotelIdResponse>>>
{
    public GetRoomsByHotelIdQuery(Guid hotelId)
    {
        HotelId = hotelId;
    }

    public Guid HotelId { get; }
}
