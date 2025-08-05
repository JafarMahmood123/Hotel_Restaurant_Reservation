using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Recommendation;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Mappings;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Recommendations.Queries.GetRecommendedRestaurantsBasedOnUserId;

public class GetRecommendedRestaurantsBasedOnUserIdQueryHandler
    : IQueryHandler<GetRecommendedRestaurantsBasedOnUserIdQuery, Result<PagedResult<RestaurantResponse>>>
{
    private readonly IRecommendationService _recommendationService;
    private readonly IRestaurantRespository _restaurantRespository;
    private readonly IGenericRepository<UserMapping> _userMappingRepository;
    private readonly IGenericRepository<RestaurantMapping> _restaurantMappingRepository;
    private readonly IMapper _mapper;

    public GetRecommendedRestaurantsBasedOnUserIdQueryHandler(
        IRecommendationService recommendationService,
        IRestaurantRespository restaurantRespository,
        IGenericRepository<UserMapping> userMappingRepository,
        IGenericRepository<RestaurantMapping> restaurantMappingRepository,
        IMapper mapper)
    {
        _recommendationService = recommendationService;
        _restaurantRespository = restaurantRespository;
        _userMappingRepository = userMappingRepository;
        _restaurantMappingRepository = restaurantMappingRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<RestaurantResponse>>> Handle(GetRecommendedRestaurantsBasedOnUserIdQuery request, CancellationToken cancellationToken)
    {

        var userMapping = await _userMappingRepository.GetFirstOrDefaultAsync(x => x.UserId.ToString() == request.UserId);
        if (userMapping is null)
        {
            return Result.Success(new PagedResult<RestaurantResponse>(new List<RestaurantResponse>(), request.Page, request.PageSize, 0));
        }
        var yelpUserId = userMapping.YelpUserId;

        var recommendedYelpBusinessIds = await _recommendationService.GetRecommendations(yelpUserId) ?? new List<string>();

        if (!recommendedYelpBusinessIds.Any())
        {
            return Result.Success(new PagedResult<RestaurantResponse>(new List<RestaurantResponse>(), request.Page, request.PageSize, 0));
        }

        var internalRestaurantIds = await _restaurantMappingRepository
            .Where(x => recommendedYelpBusinessIds.Contains(x.YelpBusinessId))
            .Select(x => x.RestaurantId)
            .ToListAsync(cancellationToken);


        var recommendedRestaurants = await _restaurantRespository
            .Where(x => internalRestaurantIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var finalRestaurantList = _mapper.Map<List<RestaurantResponse>>(recommendedRestaurants);

        if (finalRestaurantList.Count < 50)
        {
            var stillRequired = 50 - finalRestaurantList.Count;
            var existingIds = finalRestaurantList.Select(r => r.Id).ToList();

            var fillerRestaurants = await _restaurantRespository
                .Where(r => !existingIds.Contains(r.Id))
                .Take(stillRequired)
                .ToListAsync(cancellationToken);

            var fillerResponse = _mapper.Map<List<RestaurantResponse>>(fillerRestaurants);

            finalRestaurantList.AddRange(fillerResponse);
        }

        var pagedResult = new PagedResult<RestaurantResponse>(
            finalRestaurantList,
            request.Page,
            request.PageSize,
            finalRestaurantList.Count);

        return Result.Success(pagedResult);
    }
}