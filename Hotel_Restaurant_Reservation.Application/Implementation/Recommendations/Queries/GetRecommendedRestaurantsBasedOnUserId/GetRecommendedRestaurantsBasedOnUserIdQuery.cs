using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Recommendations.Queries.GetRecommendedRestaurantsBasedOnUserId;

public class GetRecommendedRestaurantsBasedOnUserIdQuery : ICommand<Result<IEnumerable<string>>>
{
    public GetRecommendedRestaurantsBasedOnUserIdQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}
