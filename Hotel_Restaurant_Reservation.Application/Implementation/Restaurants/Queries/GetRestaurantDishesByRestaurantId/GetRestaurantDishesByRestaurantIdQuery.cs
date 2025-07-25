using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantDishesByRestaurantId;

public class GetRestaurantDishesByRestaurantIdQuery : ICommand<Result<IEnumerable<RestaurantDishResponse>>>
{
    public GetRestaurantDishesByRestaurantIdQuery(Guid restaurantId)
    {
        RestaurantId = restaurantId;
    }

    public Guid RestaurantId { get; }
}
