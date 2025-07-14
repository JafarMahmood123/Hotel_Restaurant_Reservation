using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationsByCityId;

public class GetLocalLocationsByCityIdQuery : IQuery<Result<IEnumerable<LocalLocationResponse>>>
{
    public GetLocalLocationsByCityIdQuery(Guid cityId)
    {
        CityId = cityId;
    }

    public Guid CityId { get; }
}