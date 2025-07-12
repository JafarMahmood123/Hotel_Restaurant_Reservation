using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries.GetHotelReservationById;

public class GetHotelReservationByIdQuery : IQuery<Result<HotelReservationResponse>>
{
    public GetHotelReservationByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}