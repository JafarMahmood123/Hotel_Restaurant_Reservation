using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveDishesFromRestaurant;

public class RemoveDishesFromRestaurantCommandHandler
    : ICommandHandler<RemoveDishesFromRestaurantCommand, Result<List<DishWithPriceResponse>>>
{
    private readonly IGenericRepository<RestaurantDishPrice> _restaurantDishPriceRepository;
    private readonly IGenericRepository<Dish> _dishRepository;
    private readonly IRestaurantRespository _restaurantRespository;
    private readonly IMapper _mapper;

    public RemoveDishesFromRestaurantCommandHandler(
        IGenericRepository<RestaurantDishPrice> restaurantDishPriceRepository,
        IGenericRepository<Dish> dishRepository,
        IRestaurantRespository restaurantRespository,
        IMapper mapper)
    {
        _restaurantDishPriceRepository = restaurantDishPriceRepository;
        _dishRepository = dishRepository;
        this._restaurantRespository = restaurantRespository;
        _mapper = mapper;
    }

    public async Task<Result<List<DishWithPriceResponse>>> Handle(
    RemoveDishesFromRestaurantCommand request,
    CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var dishIds = request.Request.Ids;

        // Validate request
        if (dishIds == null || !dishIds.Any())
        {
            return Result.Failure<List<DishWithPriceResponse>>(
                DomainErrors.Restaurant.NoDishesToRemove);
        }

        // Get all existing dish-price associations for this restaurant
        var allRestaurantDishes = await _restaurantDishPriceRepository
            .Where(x => x.RestaurantId == restaurantId)
            .Include(x => x.Dish)
            .ToListAsync();

        // Get the specific dishes to remove
        var dishesToRemove = allRestaurantDishes
            .Where(x => dishIds.Contains(x.DishId))
            .ToList();

        if (!dishesToRemove.Any())
        {
            return Result.Failure<List<DishWithPriceResponse>>(
                DomainErrors.Restaurant.NoDishesToRemove);
        }

        // Verify all requested dishes exist
        var missingDishIds = dishIds.Except(dishesToRemove.Select(x => x.DishId)).ToList();
        if (missingDishIds.Any())
        {
            return Result.Failure<List<DishWithPriceResponse>>(
                DomainErrors.Dish.NotFound(missingDishIds.First()));
        }

        // Remove associations
        _restaurantDishPriceRepository.RemoveRange(dishesToRemove);
        await _restaurantDishPriceRepository.SaveChangesAsync();

        // Get remaining dishes after removal
        var remainingDishes = allRestaurantDishes.Except(dishesToRemove).ToList();

        // Update restaurant price information if there are remaining dishes
        if (remainingDishes.Any())
        {
            var remainingPrices = remainingDishes.Select(x => x.Price).ToList();

            // Get restaurant to update
            var restaurant = await _restaurantRespository.GetByIdAsync(restaurantId);
            if (restaurant == null)
            {
                return Result.Failure<List<DishWithPriceResponse>>(
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

        // Map to response DTOs
        var response = dishesToRemove.Select(x => new DishWithPriceResponse
        {
            Dish = _mapper.Map<DishResponse>(x.Dish),
            Price = x.Price
        }).ToList();

        return Result.Success(response);
    }
}