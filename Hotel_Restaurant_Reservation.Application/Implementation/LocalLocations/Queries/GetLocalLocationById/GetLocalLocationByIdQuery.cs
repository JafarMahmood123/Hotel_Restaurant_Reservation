using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationById;

public class GetLocalLocationByIdQuery : IQuery<Result<LocalLocationResponse>>
{
    public GetLocalLocationByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}