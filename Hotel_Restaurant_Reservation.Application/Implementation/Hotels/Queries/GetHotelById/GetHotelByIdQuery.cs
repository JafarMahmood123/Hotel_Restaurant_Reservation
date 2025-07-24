using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;

public class GetHotelByIdQuery : IQuery<Result<HotelResponse>>
{
    public Guid Id { get; set; }

    public GetHotelByIdQuery(Guid id)
    {
        Id = id;
    }
}