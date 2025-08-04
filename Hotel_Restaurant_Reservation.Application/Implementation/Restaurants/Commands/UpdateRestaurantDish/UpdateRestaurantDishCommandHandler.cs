using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurantDish
{
    /// <summary>
    /// Handles the UpdateRestaurantDishCommand.
    /// </summary>
    public class UpdateRestaurantDishCommandHandler : ICommandHandler<UpdateRestaurantDishCommand, Result>
    {
        private readonly IRestaurantRespository _restaurantRepository;
        private readonly IGenericRepository<RestaurantDish> _restaurantDishRepository;

        public UpdateRestaurantDishCommandHandler(
            IRestaurantRespository restaurantRepository,
            IGenericRepository<RestaurantDish> restaurantDishRepository)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantDishRepository = restaurantDishRepository;
        }

        /// <summary>
        /// Handles the command to update a restaurant's dish and recalculate the restaurant's price range.
        /// </summary>
        public async Task<Result> Handle(UpdateRestaurantDishCommand request, CancellationToken cancellationToken)
        {
            // First, find the specific dish to update.
            var restaurantDish = await _restaurantDishRepository.GetFirstOrDefaultAsync(
                rd => rd.RestaurantId == request.RestaurantId && rd.DishId == request.DishId);

            if (restaurantDish is null)
            {
                return Result.Failure(new Error("Dish.NotFound", "The specified dish was not found for this restaurant."));
            }

            // Update the dish properties.
            restaurantDish.Price = request.UpdateRestaurantDishRequest.Price;
            restaurantDish.Description = request.UpdateRestaurantDishRequest.Description;
            restaurantDish.PictureUrl = request.UpdateRestaurantDishRequest.PictureUrl;

            await _restaurantDishRepository.UpdateAsync(restaurantDish.Id, restaurantDish);

            // After updating the dish, find the parent restaurant.
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
            {
                // This case should be rare if the dish was found, but it's good practice to check.
                return Result.Failure(new Error("Restaurant.NotFound", "The parent restaurant could not be found."));
            }

            // Get all dishes for this restaurant to recalculate min/max prices.
            var allRestaurantDishes = await _restaurantDishRepository
                .Where(rd => rd.RestaurantId == request.RestaurantId)
                .ToListAsync(cancellationToken);

            if (allRestaurantDishes.Any())
            {
                // Recalculate and update the restaurant's price range.
                restaurant.MinPrice = allRestaurantDishes.Min(rd => rd.Price);
                restaurant.MaxPrice = allRestaurantDishes.Max(rd => rd.Price);
                await _restaurantRepository.UpdateAsync(restaurant.Id, restaurant);
            }

            // Save all changes (dish update and restaurant price update) in a single transaction.
            await _restaurantDishRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
