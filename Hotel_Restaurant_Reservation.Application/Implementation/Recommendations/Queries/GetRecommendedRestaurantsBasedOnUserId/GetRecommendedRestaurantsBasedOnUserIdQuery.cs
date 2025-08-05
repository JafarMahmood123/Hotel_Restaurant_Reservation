using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Recommendations.Queries.GetRecommendedRestaurantsBasedOnUserId;

public class GetRecommendedRestaurantsBasedOnUserIdQuery : IQuery<Result<PagedResult<RestaurantResponse>>>
{
    public GetRecommendedRestaurantsBasedOnUserIdQuery(string userId, int page, int pageSize)
    {
        UserId = userId;
        Page = page;
        PageSize = pageSize;
    }

    public string UserId { get; }
    public int Page { get; }
    public int PageSize { get; }
}