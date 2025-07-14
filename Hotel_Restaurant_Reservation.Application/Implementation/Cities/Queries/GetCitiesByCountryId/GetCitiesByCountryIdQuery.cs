using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCitiesByCountryId;

public class GetCitiesByCountryIdQuery : IQuery<Result<IEnumerable<CityResponse>>>
{
    public GetCitiesByCountryIdQuery(Guid countryId)
    {
        CountryId = countryId;
    }

    public Guid CountryId { get; }
}