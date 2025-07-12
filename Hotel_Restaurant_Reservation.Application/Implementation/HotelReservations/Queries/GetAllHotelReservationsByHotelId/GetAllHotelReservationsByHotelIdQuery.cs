using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries.GetAllHotelReservationsByHotelId;

public class GetAllHotelReservationsByHotelIdQuery : IQuery<Result<IEnumerable<HotelReservationResponse>>>
{
    public GetAllHotelReservationsByHotelIdQuery(Guid hotelId)
    {
        HotelId = hotelId;
    }

    public Guid HotelId { get; }
}