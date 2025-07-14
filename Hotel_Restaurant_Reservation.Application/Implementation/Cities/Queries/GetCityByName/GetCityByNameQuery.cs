using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityByName;

public class GetCityByNameQuery : IQuery<Result<CityResponse>>
{
    public GetCityByNameQuery(string name)
    {
        Name = name;
    }

    public string Name { get; }
}