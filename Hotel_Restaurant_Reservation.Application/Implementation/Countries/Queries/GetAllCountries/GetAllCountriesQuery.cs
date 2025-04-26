using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetAllCountries;

public class GetAllCountriesQuery : IQuery<IEnumerable<Country>?>
{
}
