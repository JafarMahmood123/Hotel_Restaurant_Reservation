using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetCountryByName;

public class GetCountryByNameQuery : IQuery<Country?>
{
    public string Name { get; set; }

    public GetCountryByNameQuery(string name)
    {
        Name = name;
    }
}
