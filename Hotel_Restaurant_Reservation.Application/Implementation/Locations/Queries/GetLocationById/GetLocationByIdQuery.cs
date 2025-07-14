using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetLocationById;

public class GetLocationByIdQuery : IQuery<Result<LocationResponse>>
{
    public GetLocationByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}