using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRestaurantImagesByRestaurantId
{
    /// <summary>
    /// Handles the GetRestaurantImagesByRestaurantIdQuery.
    /// </summary>
    public class GetRestaurantImagesByRestaurantIdQueryHandler : IQueryHandler<GetRestaurantImagesByRestaurantIdQuery, Result<List<string>>>
    {
        private readonly IGenericRepository<RestaurantImage> _restaurantImageRepository;
        private readonly IRestaurantRespository _restaurantRepository;

        public GetRestaurantImagesByRestaurantIdQueryHandler(IGenericRepository<RestaurantImage> restaurantImageRepository, IRestaurantRespository restaurantRepository)
        {
            _restaurantImageRepository = restaurantImageRepository;
            _restaurantRepository = restaurantRepository;
        }

        /// <summary>
        /// Handles the request to fetch image URLs for a given restaurant.
        /// </summary>
        public async Task<Result<List<string>>> Handle(GetRestaurantImagesByRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            // First, check if the restaurant exists.
            var restaurantExists = await _restaurantRepository.GetFirstOrDefaultAsync(r => r.Id == request.RestaurantId);
            if (restaurantExists == null)
            {
                // If not, return a failure result.
                return Result.Failure<List<string>>(new Error("Restaurant.NotFound", $"The restaurant with ID {request.RestaurantId} was not found."));
            }

            // Fetch all images for the given restaurant ID.
            var imageUrls = await _restaurantImageRepository
                .Where(img => img.RestaurantId == request.RestaurantId)
                .Select(img => img.Url) // Select only the URL property
                .ToListAsync(cancellationToken);

            // Return a success result with the list of URLs.
            // If no images are found, this will correctly return an empty list.
            return Result.Success(imageUrls);
        }
    }
}
