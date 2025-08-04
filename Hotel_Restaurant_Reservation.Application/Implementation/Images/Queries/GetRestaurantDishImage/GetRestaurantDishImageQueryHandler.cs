using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRestaurantDishImage
{
    /// <summary>
    /// Handles the GetRestaurantDishImageQuery.
    /// </summary>
    public class GetRestaurantDishImageQueryHandler : IQueryHandler<GetRestaurantDishImageQuery, Result<string>>
    {
        private readonly IGenericRepository<RestaurantDish> _restaurantDishRepository;
        private readonly IRestaurantRespository _restaurantRespository;
        private readonly IGenericRepository<Dish> _dishRepository;

        public GetRestaurantDishImageQueryHandler(IGenericRepository<RestaurantDish> restaurantDishRepository,
            IRestaurantRespository restaurantRespository, IGenericRepository<Dish> dishRepository)
        {
            _restaurantDishRepository = restaurantDishRepository;
            _restaurantRespository = restaurantRespository;
            _dishRepository = dishRepository;
        }

        /// <summary>
        /// Handles the request to fetch an image URL for a given dish.
        /// </summary>
        public async Task<Result<string>> Handle(GetRestaurantDishImageQuery request, CancellationToken cancellationToken)
        {
            // First, check if the restaurant exists.
            var restaurantExists = await _restaurantRespository.GetFirstOrDefaultAsync(r => r.Id == request.RestaurantId);
            if (restaurantExists == null)
            {
                return Result.Failure<string>(new Error("Restaurant.NotFound", $"The restaurant with ID {request.RestaurantId} was not found."));
            }

            // Then, check if the dish exists within that restaurant.
            var dishExists = await _dishRepository.GetFirstOrDefaultAsync(d => d.Id == request.DishId);
            if (dishExists == null)
            {
                return Result.Failure<string>(new Error("Dish.NotFound", $"The dish with ID {request.DishId} was not found in restaurant {request.RestaurantId}."));
            }

            var restaurantDish = await _restaurantDishRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == request.RestaurantId
            && x.DishId == request.DishId);

            if (restaurantDish == null)
            {
                return Result.Failure<string>(DomainErrors.Restaurant.DontHaveDish);
            }


            // Return a success result with the URL.
            return Result.Success(restaurantDish.PictureUrl);
        }
    }
}