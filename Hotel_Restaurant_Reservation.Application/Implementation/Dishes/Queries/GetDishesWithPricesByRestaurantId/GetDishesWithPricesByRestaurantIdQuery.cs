using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries.GetDishesWithPricesByRestaurantId;

public class GetDishesWithPricesByRestaurantIdQuery : IQuery<Result<IEnumerable<RestaurantDishResponse>>>
{
    public GetDishesWithPricesByRestaurantIdQuery(Guid restaurantId)
    {
        RestaurantId = restaurantId;
    }

    public Guid RestaurantId { get; }
}