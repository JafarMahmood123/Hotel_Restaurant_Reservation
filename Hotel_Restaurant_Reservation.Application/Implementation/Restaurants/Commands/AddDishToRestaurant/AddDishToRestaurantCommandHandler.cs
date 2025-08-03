using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishToRestaurant;

public class AddDishToRestaurantCommandHandler
    : ICommandHandler<AddDishToRestaurantCommand, Result<RestaurantDishResponse>>
{
    private readonly IGenericRepository<Dish> _dishRepository;
    private readonly IGenericRepository<RestaurantDish> _restaurantDishRepository;
    private readonly IRestaurantRespository _restaurantRepository;

    public AddDishToRestaurantCommandHandler(
        IGenericRepository<Dish> dishRepository,
        IGenericRepository<RestaurantDish> restaurantDishRepository,
        IRestaurantRespository restaurantRepository)
    {
        _dishRepository = dishRepository;
        _restaurantDishRepository = restaurantDishRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<Result<RestaurantDishResponse>> Handle(
        AddDishToRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Validate Restaurant and Dish exist
        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null)
        {
            return Result.Failure<RestaurantDishResponse>(DomainErrors.Restaurant.NotFound(request.RestaurantId));
        }

        var dish = await _dishRepository.GetByIdAsync(request.AddDishToRestaurantRequest.DishId);
        if (dish is null)
        {
            return Result.Failure<RestaurantDishResponse>(DomainErrors.Dish.NotFound(request.AddDishToRestaurantRequest.DishId));
        }

        // 2. Check if the dish is already associated with the restaurant
        var dishAlreadyExists = await _restaurantDishRepository.Where(
            rd => rd.RestaurantId == request.RestaurantId && rd.DishId == request.AddDishToRestaurantRequest.DishId)
            .ToListAsync();

        if (dishAlreadyExists.Any())
        {
            return Result.Failure<RestaurantDishResponse>(
                DomainErrors.Restaurant.DishAlreadyExists);
        }

        // 3. Create and add the new RestaurantDish entity
        var newRestaurantDish = new RestaurantDish
        {
            RestaurantId = request.RestaurantId, 
            DishId = request.AddDishToRestaurantRequest.DishId,
            Price = request.AddDishToRestaurantRequest.Price, 
            Description = request.AddDishToRestaurantRequest.Description,
            PictureUrl = request.AddDishToRestaurantRequest.PictureUrl
        };

        await _restaurantDishRepository.AddAsync(newRestaurantDish);
        await _restaurantDishRepository.SaveChangesAsync();

        // 4. Recalculate restaurant's price level
        var allRestaurantDishes = await _restaurantDishRepository
            .Where(rd => rd.RestaurantId == request.RestaurantId)
            .ToListAsync(cancellationToken);

        var allPrices = allRestaurantDishes.Select(rd => rd.Price).ToList();
        restaurant.MaxPrice = allPrices.Max();
        restaurant.MinPrice = allPrices.Min();
        var averagePrice = allPrices.Average();

        restaurant.PriceLevel = averagePrice switch
        {
            < 15 => RestaurantPriceLevel.Low,
            >= 15 and < 30 => RestaurantPriceLevel.Medium,
            >= 30 and < 60 => RestaurantPriceLevel.High,
            >= 60 => RestaurantPriceLevel.Luxury,
            _ => RestaurantPriceLevel.NotSet
        };

        await _restaurantRepository.SaveChangesAsync();

        // 5. Create and return the successful response
        var response = new RestaurantDishResponse
        {
            Id = dish.Id, 
            Name = dish.Name, 
            Price = newRestaurantDish.Price, 
            Description = newRestaurantDish.Description, 
            PictureUrl = newRestaurantDish.PictureUrl
        };

        return Result.Success(response);
    }
}