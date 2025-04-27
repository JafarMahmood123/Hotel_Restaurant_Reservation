using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetAllLocations;

public class GetAllLocationsQuery : IQuery<IEnumerable<Location>?>
{
}
