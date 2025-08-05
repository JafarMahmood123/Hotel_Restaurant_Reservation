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
    /// <summary>
    /// Handles the retrieval and pagination of restaurants based on filter criteria.
    /// This handler leverages IQueryable for optimal database performance.
    /// </summary>
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
                // 1. Get the base IQueryable from the repository. No database call is made here.
                var restaurantsQuery = _restaurantRepository.GetFilteredRestaurantsQuery(
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

                // 2. Get the total count for pagination metadata.
                // This executes a fast `SELECT COUNT(*)` query on the filtered set.
                var totalCount = await restaurantsQuery.CountAsync(cancellationToken);

                // 3. Apply pagination to the IQueryable. This modifies the SQL query
                //    to use OFFSET and FETCH (or equivalent) for efficient data retrieval.
                var pagedRestaurants = await restaurantsQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken); // This executes the final query to get only the items for the current page.

                // 4. Map the results to the response DTO
                var responseItems = _mapper.Map<List<RestaurantResponse>>(pagedRestaurants);

                // 5. Create the PagedResult object.
                // By passing totalCount and pageSize here, the TotalPages property
                // in your PagedResult class will be calculated automatically.
                var pagedResult = new PagedResult<RestaurantResponse>(
                    responseItems,
                    request.Page,
                    request.PageSize,
                    totalCount);

                return Result.Success(pagedResult);
            }
            catch (Exception ex)
            {
                // Return a failure result if any exception occurs during the process.
                return Result.Failure<PagedResult<RestaurantResponse>>(
                    new Error("Restaurant.QueryError", $"An error occurred while retrieving restaurants: {ex.Message}"));
            }
        }
    }
}
