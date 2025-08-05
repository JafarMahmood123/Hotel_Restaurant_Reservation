using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler
        : IQueryHandler<GetAllRestaurantsQuery, Result<PagedResult<RestaurantResponse>>>
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

        public async Task<Result<PagedResult<RestaurantResponse>>> Handle(
            GetAllRestaurantsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                // --- C H A N G E: Pass the new SubName property to the filtering method ---
                var restaurantsQuery = _restaurantRepository.GetFilteredRestaurantsQuery(
                    request.SubName,
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

                var totalCount = await restaurantsQuery.CountAsync(cancellationToken);

                var pagedRestaurants = await restaurantsQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                var responseItems = _mapper.Map<List<RestaurantResponse>>(pagedRestaurants);

                var pagedResult = new PagedResult<RestaurantResponse>(
                    responseItems,
                    request.Page,
                    request.PageSize,
                    totalCount);

                return Result.Success(pagedResult);
            }
            catch (Exception ex)
            {
                return Result.Failure<PagedResult<RestaurantResponse>>(
                    new Error("Restaurant.QueryError", $"An error occurred while retrieving restaurants: {ex.Message}"));
            }
        }
    }
}
