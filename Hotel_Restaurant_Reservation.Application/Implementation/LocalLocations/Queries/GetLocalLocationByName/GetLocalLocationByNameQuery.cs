using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationByName;

public class GetLocalLocationByNameQuery : IQuery<Result<LocalLocationResponse>>
{
    public GetLocalLocationByNameQuery(string name)
    {
        Name = name;
    }

    public string Name { get; }
}