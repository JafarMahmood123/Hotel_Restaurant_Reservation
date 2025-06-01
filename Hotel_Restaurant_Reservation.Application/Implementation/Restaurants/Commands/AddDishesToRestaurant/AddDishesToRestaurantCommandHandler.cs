using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishesToRestaurant;

public class AddDishesToRestaurantCommandHandler
    : ICommandHandler<AddDishesToRestaurantCommand, Result<List<DishWithPriceResponse>>>
{
    private readonly IGenericRepository<Dish> _dishRepository;
    private readonly IGenericRepository<RestaurantDishPrice> _dishPriceRepository;
    private readonly IRestaurantRespository _restaurantRespository;
    private readonly IMapper _mapper;

    public AddDishesToRestaurantCommandHandler(
        IGenericRepository<Dish> dishRepository,
        IGenericRepository<RestaurantDishPrice> dishPriceRepository,
        IRestaurantRespository restaurantRespository,
        IMapper mapper)
    {
        _dishRepository = dishRepository;
        _dishPriceRepository = dishPriceRepository;
        _restaurantRespository = restaurantRespository;
        _mapper = mapper;
    }

    public async Task<Result<List<DishWithPriceResponse>>> Handle(
    AddDishesToRestaurantCommand request,
    CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var dishPrices = request.DishIdsWithPrices.dishIdsWithPrices;

        // Validate dish prices dictionary is not empty
        if (dishPrices == null || dishPrices.Count == 0)
        {
            return Result.Failure<List<DishWithPriceResponse>>(
                DomainErrors.Restaurant.NoDishesWithPricesProvided);
        }

        List<Dish> dishes = new();
        List<RestaurantDishPrice> dishPricesToAdd = new();

        // Verify all dishes exist and collect them
        foreach (var dishPrice in dishPrices)
        {
            var dish = await _dishRepository.GetByIdAsync(dishPrice.Key);
            if (dish == null)
            {
                return Result.Failure<List<DishWithPriceResponse>>(
                    DomainErrors.Dish.NotFound(dishPrice.Key));
            }
            dishes.Add(dish);
        }

        // Get all existing dish prices for this restaurant
        var existingDishPrices = await _dishPriceRepository.Where(
            x => x.RestaurantId == restaurantId).ToListAsync();

        // Create dictionary of all dish prices (existing + new)
        var allDishPrices = existingDishPrices
            .ToDictionary(x => x.DishId, x => x.Price);

        // Add or update with new dish prices
        foreach (var dishPrice in dishPrices)
        {
            allDishPrices[dishPrice.Key] = dishPrice.Value;
        }

        // Create or update dish prices in database
        foreach (var dishPrice in dishPrices)
        {
            var existingDishWithPrice = existingDishPrices
                .FirstOrDefault(x => x.DishId == dishPrice.Key);

            if (existingDishWithPrice == null)
            {
                var newDishPrice = new RestaurantDishPrice
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = restaurantId,
                    DishId = dishPrice.Key,
                    Price = dishPrice.Value
                };
                await _dishPriceRepository.AddAsync(newDishPrice);
            }
            else
            {
                existingDishWithPrice.Price = dishPrice.Value;
            }
        }

        await _dishPriceRepository.SaveChangesAsync();

        var restaurant = await _restaurantRespository.GetByIdAsync(restaurantId);
        if (restaurant == null)
            return Result.Failure<List<DishWithPriceResponse>>(DomainErrors.Restaurant.NotFound(restaurantId));

        // Calculate prices based on ALL dishes (existing + new)
        var allPrices = allDishPrices.Values.ToList();

        restaurant.MaxPrice = allPrices.Max();
        restaurant.MinPrice = allPrices.Min();
        var averagePrice = allPrices.Average();

        // Set price level based on average price
        restaurant.PriceLevel = averagePrice switch
        {
            < 15 => RestaurantPriceLevel.Low,
            >= 15 and < 30 => RestaurantPriceLevel.Medium,
            >= 30 and < 60 => RestaurantPriceLevel.High,
            >= 60 => RestaurantPriceLevel.Luxury,
            _ => RestaurantPriceLevel.NotSet
        };

        await _restaurantRespository.SaveChangesAsync();

        // Map to response DTOs (only for the newly added dishes)
        var response = dishes.Select(dish => new DishWithPriceResponse
        {
            Dish = _mapper.Map<DishResponse>(dish),
            Price = dishPrices[dish.Id]
        }).ToList();

        return Result.Success(response);
    }
}