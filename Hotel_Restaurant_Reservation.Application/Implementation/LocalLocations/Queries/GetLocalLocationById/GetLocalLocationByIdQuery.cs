using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationById;

public class GetLocalLocationByIdQuery : IQuery<LocalLocation?>
{
    public GetLocalLocationByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
