using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Recommendation;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Recommendations.Queries.GetRecommendedRestaurantsBasedOnUserId;

public class GetRecommendedRestaurantsBasedOnUserIdQueryHandler
    : IQueryHandler<GetRecommendedRestaurantsBasedOnUserIdQuery, Result<IEnumerable<RestaurantResponse>>>
{
    private readonly IRecommendationService _recommendationService;
    private readonly IRestaurantRespository _restaurantRespository;
    private readonly IMapper _mapper;

    public GetRecommendedRestaurantsBasedOnUserIdQueryHandler(
        IRecommendationService recommendationService,
        IRestaurantRespository restaurantRespository,
        IMapper mapper)
    {
        _recommendationService = recommendationService;
        _restaurantRespository = restaurantRespository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<RestaurantResponse>>> Handle(GetRecommendedRestaurantsBasedOnUserIdQuery request, CancellationToken cancellationToken)
    {
        var recommendations = await _recommendationService.GetRecommendations(request.UserId);

        var restaurants = await _restaurantRespository.Where(x => recommendations.Contains(x.Id.ToString())).ToListAsync();

        var restaurantResponse = _mapper.Map<List<RestaurantResponse>>(restaurants);

        if (restaurants.Count >= 50)
            return Result.Success((IEnumerable<RestaurantResponse>)restaurantResponse);

        var stillRequiredRestaurants = 50 - restaurants.Count;

        var restaurantCompletion = await _restaurantRespository.Take(stillRequiredRestaurants);
        restaurantCompletion = restaurantCompletion.Take(stillRequiredRestaurants);

        var restaurantCompletionResponse = _mapper.Map<List<RestaurantResponse>>(restaurantCompletion);

        return Result.Success(restaurantResponse.Concat(restaurantCompletionResponse));
    }
}