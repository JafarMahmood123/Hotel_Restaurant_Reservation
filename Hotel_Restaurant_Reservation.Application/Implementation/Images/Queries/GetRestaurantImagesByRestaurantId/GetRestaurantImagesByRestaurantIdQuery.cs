using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRestaurantImagesByRestaurantId;

public class GetRestaurantImagesByRestaurantIdQuery : IQuery<Result<List<string>>>
{
    public GetRestaurantImagesByRestaurantIdQuery(Guid restaurantId)
    {
        RestaurantId = restaurantId;
    }

    public Guid RestaurantId { get; }
}