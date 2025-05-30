using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveDishesFromRestaurant;

public class RemoveDishesFromRestaurantCommandHandler
    : ICommandHandler<RemoveDishesFromRestaurantCommand, Result<List<DishWithPriceResponse>>>
{
    private readonly IGenericRepository<RestaurantDishPrice> _restaurantDishPriceRepository;
    private readonly IGenericRepository<Dish> _dishRepository;
    private readonly IMapper _mapper;

    public RemoveDishesFromRestaurantCommandHandler(
        IGenericRepository<RestaurantDishPrice> restaurantDishPriceRepository,
        IGenericRepository<Dish> dishRepository,
        IMapper mapper)
    {
        _restaurantDishPriceRepository = restaurantDishPriceRepository;
        _dishRepository = dishRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<DishWithPriceResponse>>> Handle(
        RemoveDishesFromRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var dishIds = request.Request.Ids;

        // Get all existing dish-price associations
        var restaurantDishes = await _restaurantDishPriceRepository
            .Where(x => x.RestaurantId == restaurantId && dishIds.Contains(x.DishId))
            .Include(x => x.Dish)
            .ToListAsync();

        if (!restaurantDishes.Any())
        {
            return Result.Failure<List<DishWithPriceResponse>>(
                DomainErrors.Restaurant.NoDishesToRemove);
        }

        // Verify all dishes exist
        var missingDishIds = dishIds.Except(restaurantDishes.Select(x => x.DishId)).ToList();
        if (missingDishIds.Any())
        {
            return Result.Failure<List<DishWithPriceResponse>>(
                DomainErrors.Dish.NotFound(missingDishIds.First()));
        }

        // Remove associations
        _restaurantDishPriceRepository.RemoveRange(restaurantDishes);
        await _restaurantDishPriceRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = restaurantDishes.Select(x => new DishWithPriceResponse
        {
            Dish = _mapper.Map<DishResponse>(x.Dish),
            Price = x.Price
        }).ToList();

        return Result.Success(response);
    }
}