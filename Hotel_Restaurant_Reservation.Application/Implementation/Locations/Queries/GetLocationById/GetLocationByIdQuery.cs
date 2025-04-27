using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetLocationById;

public class GetLocationByIdQuery : IQuery<Location?>
{
    public GetLocationByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
