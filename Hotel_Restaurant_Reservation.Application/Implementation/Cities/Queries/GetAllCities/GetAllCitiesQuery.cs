using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetAllCities;

public class GetAllCitiesQuery : IQuery<IEnumerable<City>?>
{
}
