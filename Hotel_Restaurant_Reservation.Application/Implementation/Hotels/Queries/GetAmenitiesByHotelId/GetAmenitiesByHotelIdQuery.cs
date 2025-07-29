using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAmenitiesByHotelId;

public class GetAmenitiesByHotelIdQuery : IQuery<Result<IEnumerable<GetAmenitiesByHotelIdResponse>>>
{
    public GetAmenitiesByHotelIdQuery(Guid hotelId)
    {
        HotelId = hotelId;
    }

    public Guid HotelId { get; }
}
