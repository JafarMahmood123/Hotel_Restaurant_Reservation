using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetAllLocations;

public class GetAllLocationsQuery : IQuery<Result<IEnumerable<LocationResponse>>>
{
}