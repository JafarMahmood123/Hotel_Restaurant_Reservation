using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery : IQuery<Restaurant?>
{
    public Guid Id { get; set; }

    public GetRestaurantByIdQuery(Guid id)
    {
        Id = id;
    }
}
