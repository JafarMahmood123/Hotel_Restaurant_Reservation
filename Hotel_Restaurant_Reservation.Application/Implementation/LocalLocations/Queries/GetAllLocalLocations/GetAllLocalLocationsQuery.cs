using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetAllLocalLocations;

public class GetAllLocalLocationsQuery : IQuery<IEnumerable<LocalLocation>?>
{
}
