using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery : IQuery<Result<RestaurantResponse>>
{
    public Guid Id { get; }

    public GetRestaurantByIdQuery(Guid id)
    {
        Id = id;
    }
}