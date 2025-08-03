using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveDishesFromRestaurant;

public class RemoveDishFromRestaurantCommandHandler
    : ICommandHandler<RemoveDishFromRestaurantCommand, Result>
{
    private readonly IGenericRepository<RestaurantDish> _restaurantDishRepository;
    private readonly IGenericRepository<Dish> _dishRepository;
    private readonly IRestaurantRespository _restaurantRespository;
    private readonly IMapper _mapper;

    public RemoveDishFromRestaurantCommandHandler(IGenericRepository<RestaurantDish> restaurantDishRepository,
        IGenericRepository<Dish> dishRepository,IRestaurantRespository restaurantRespository, IMapper mapper)
    {
        _restaurantDishRepository = restaurantDishRepository;
        _dishRepository = dishRepository;
        _restaurantRespository = restaurantRespository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(RemoveDishFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var dishId = request.DishId;

        var restaurantDish = await _restaurantDishRepository.GetFirstOrDefaultAsync(x => x.DishId == dishId 
        && x.RestaurantId == restaurantId);

        // Remove associations
        await _restaurantDishRepository.RemoveAsync(restaurantDish.Id);
        await _restaurantDishRepository.SaveChangesAsync();

        // Get remaining dishes after removal
        var remainingDishes = await _restaurantDishRepository.Where(x => x.RestaurantId == restaurantId)
            .ToListAsync();

        // Update restaurant price information if there are remaining dishes
        if (remainingDishes.Any())
        {
            var remainingPrices = remainingDishes.Select(x => x.Price).ToList();

            // Get restaurant to update
            var restaurant = await _restaurantRespository.GetByIdAsync(restaurantId);
            if (restaurant == null)
            {
                return Result.Failure<List<RestaurantDishResponse>>(
                    DomainErrors.Restaurant.NotFound(restaurantId));
            }

            // Update min and max prices
            restaurant.MinPrice = remainingPrices.Min();
            restaurant.MaxPrice = remainingPrices.Max();

            // Calculate new average price and update price level
            var averagePrice = remainingPrices.Average();
            restaurant.PriceLevel = averagePrice switch
            {
                < 15 => RestaurantPriceLevel.Low,
                >= 15 and < 30 => RestaurantPriceLevel.Medium,
                >= 30 and < 60 => RestaurantPriceLevel.High,
                >= 60 => RestaurantPriceLevel.Luxury,
                _ => RestaurantPriceLevel.NotSet
            };

            await _restaurantRespository.SaveChangesAsync();
        }
        else
        {
            // If no dishes left, reset price information
            var restaurant = await _restaurantRespository.GetByIdAsync(restaurantId);
            if (restaurant != null)
            {
                restaurant.MinPrice = 0;
                restaurant.MaxPrice = 0;
                restaurant.PriceLevel = RestaurantPriceLevel.NotSet;
                await _restaurantRespository.SaveChangesAsync();
            }
        }

        return Result.Success();
    }
}