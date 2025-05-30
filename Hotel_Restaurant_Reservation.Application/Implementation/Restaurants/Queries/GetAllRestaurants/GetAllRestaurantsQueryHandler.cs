using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler
    : IQueryHandler<GetAllRestaurantsQuery, Result<List<RestaurantResponse>>>
{
    private readonly IRestaurantRespository _restaurantRepository;
    private readonly IMapper _mapper;

    public GetAllRestaurantsQueryHandler(
        IRestaurantRespository restaurantRepository,
        IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<RestaurantResponse>>> Handle(
        GetAllRestaurantsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Validate price range
            //if (request.MinPrice > request.MaxPrice)
            //{
            //    return Result.Failure<List<RestaurantResponse>>(
            //        DomainErrors.Restaurant.InvalidPriceRange);
            //}

            //// Validate star rating range
            //if (request.MinStarRating > request.MaxStarRating)
            //{
            //    return Result.Failure<List<RestaurantResponse>>(
            //        DomainErrors.Restaurant.InvalidStarRatingRange);
            //}

            var restaurants = await _restaurantRepository.GetFilteredRestaurantsAsync(
                request.TagId,
                request.FeatureId,
                request.CuisineId,
                request.DishId,
                request.MealTypeId,
                request.CountryId,
                request.CityId,
                request.LocalLocationId,
                request.MinPrice,
                request.MaxPrice,
                request.MinStarRating,
                request.MaxStarRating);

            if (restaurants == null || !restaurants.Any())
            {
                return Result.Failure<List<RestaurantResponse>>(
                    DomainErrors.Restaurant.NotFoundForFilters);
            }

            // Map to response DTOs
            var response = _mapper.Map<List<RestaurantResponse>>(restaurants);
            return Result.Success(response);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<RestaurantResponse>>(
                new Error("Restaurant.QueryError", $"Error retrieving restaurants: {ex.Message}"));
        }
    }
}