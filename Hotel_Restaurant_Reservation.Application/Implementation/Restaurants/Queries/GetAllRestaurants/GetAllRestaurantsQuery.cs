using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IQuery<IEnumerable<Restaurant>?>
{
}
