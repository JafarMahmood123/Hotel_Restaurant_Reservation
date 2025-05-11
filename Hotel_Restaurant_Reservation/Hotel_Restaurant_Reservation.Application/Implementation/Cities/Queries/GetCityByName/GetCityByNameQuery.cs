using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityByName;

public class GetCityByNameQuery : IQuery<City?>
{
    public string Name { get; set; }

    public GetCityByNameQuery(string name)
    {
        Name = name;
    }
}
