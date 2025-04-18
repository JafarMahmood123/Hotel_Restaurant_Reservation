using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;


// Need Editing Later
public class GetHotelByIdQuery : IQuery<Hotel?> 
{
    public Guid Id { get; set; }

    public GetHotelByIdQuery(Guid id)
    {
        Id = id;
    }
}
