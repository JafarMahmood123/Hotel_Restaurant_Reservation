using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantCuisinesByRestaurantId;

public class GetRestaurantCuisinesByRestaurantIdQuery : 
    IQuery<Result<IEnumerable<GetRestaurantCuisinesByRestaurantIdResponse>>>
{
    public GetRestaurantCuisinesByRestaurantIdQuery(Guid restaurantId)
    {
        RestaurantId = restaurantId;
    }

    public Guid RestaurantId { get; }
}
