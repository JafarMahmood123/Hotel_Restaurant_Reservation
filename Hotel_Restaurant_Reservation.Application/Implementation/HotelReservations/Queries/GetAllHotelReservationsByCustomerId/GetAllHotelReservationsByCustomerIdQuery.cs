using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries.GetAllHotelReservationsByCustomerId;

public class GetAllHotelReservationsByCustomerIdQuery : IQuery<Result<IEnumerable<HotelReservationResponse>>>
{
    public GetAllHotelReservationsByCustomerIdQuery(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; }
}