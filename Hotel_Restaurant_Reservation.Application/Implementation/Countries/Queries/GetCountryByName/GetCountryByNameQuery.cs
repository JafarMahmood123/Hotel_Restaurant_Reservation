using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetCountryByName;

public class GetCountryByNameQuery : IQuery<Result<CountryResponse>>
{
    public GetCountryByNameQuery(string name)
    {
        Name = name;
    }

    public string Name { get; }
}