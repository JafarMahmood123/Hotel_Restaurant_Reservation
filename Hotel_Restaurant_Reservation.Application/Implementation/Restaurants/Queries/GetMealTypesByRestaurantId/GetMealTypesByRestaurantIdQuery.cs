using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetMealTypesByRestaurantId;
using Hotel_Restaurant_Reservation.Domain.Shared;

public class GetMealTypesByRestaurantIdQuery : IQuery<Result<IEnumerable<GetMealTypesByRestaurantIdResponse>>>
{
    public GetMealTypesByRestaurantIdQuery(Guid restaurantId)
    {
        RestaurantId = restaurantId;
    }

    public Guid RestaurantId { get; }
}