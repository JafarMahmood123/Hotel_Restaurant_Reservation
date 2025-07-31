using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Recommendation;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Recommendations.Queries.GetRecommendedRestaurantsBasedOnUserId;

public class GetRecommendedRestaurantsBasedOnUserIdQueryHandler
    : ICommandHandler<GetRecommendedRestaurantsBasedOnUserIdQuery, Result<IEnumerable<string>>>
{
    private readonly IRecommendationService _recommendationService;
    private readonly IRestaurantRespository _restaurantRespository;

    public GetRecommendedRestaurantsBasedOnUserIdQueryHandler(IRecommendationService recommendationService,
        IRestaurantRespository restaurantRespository)
    {
        _recommendationService = recommendationService;
        _restaurantRespository = restaurantRespository;
    }

    public async Task<Result<IEnumerable<string>>> Handle(GetRecommendedRestaurantsBasedOnUserIdQuery request, CancellationToken cancellationToken)
    {
        var recommendations = await _recommendationService.GetRecommendations(request.UserId);

        return Result.Success(recommendations);
    }
}
