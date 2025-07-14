using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetAllCities;

public class GetAllCitiesQuery : IQuery<Result<IEnumerable<CityResponse>>>
{
}