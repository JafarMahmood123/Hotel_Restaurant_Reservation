using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationByName;

public class GetLocalLocationByNameQuery : IQuery<LocalLocation?>
{
    public GetLocalLocationByNameQuery(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
