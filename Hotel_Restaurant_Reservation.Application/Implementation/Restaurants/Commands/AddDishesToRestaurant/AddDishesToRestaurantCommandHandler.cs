using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishesToRestaurant;

public class AddDishesToRestaurantCommandHandler
    : ICommandHandler<AddDishesToRestaurantCommand, Result<List<DishWithPriceResponse>>>
{
    private readonly IGenericRepository<Dish> _dishRepository;
    private readonly IGenericRepository<RestaurantDishPrice> _dishPriceRepository;
    private readonly IMapper _mapper;

    public AddDishesToRestaurantCommandHandler(
        IGenericRepository<Dish> dishRepository,
        IGenericRepository<RestaurantDishPrice> dishPriceRepository,
        IMapper mapper)
    {
        _dishRepository = dishRepository;
        _dishPriceRepository = dishPriceRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<DishWithPriceResponse>>> Handle(
        AddDishesToRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var dishPrices = request.DishIdsWithPrices.dishIdsWithPrices;

        List<Dish> dishes = new();
        List<RestaurantDishPrice> dishPricesToAdd = new();

        // Verify all dishes exist and collect them
        foreach (var dishPrice in dishPrices)
        {
            var dish = await _dishRepository.GetByIdAsync(dishPrice.Key);
            if (dish == null)
            {
                return Result.Failure<List<DishWithPriceResponse>>(
                    DomainErrors.Dish.NotExistingDish(dishPrice.Key));
            }
            dishes.Add(dish);
        }

        // Create or update dish prices
        foreach (var dishPrice in dishPrices)
        {
            var existingDishWithPrice = await _dishPriceRepository.GetFirstOrDefaultAsync(
                x => x.RestaurantId == restaurantId && x.DishId == dishPrice.Key);

            if (existingDishWithPrice == null)
            {
                var newDishPrice = new RestaurantDishPrice
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = restaurantId,
                    DishId = dishPrice.Key,
                    Price = dishPrice.Value
                };
                dishPricesToAdd.Add(newDishPrice);
            }
        }

        if (dishPricesToAdd.Any())
        {
            await _dishPriceRepository.AddRangeAsync(dishPricesToAdd);
        }

        await _dishPriceRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = dishes.Select(dish => new DishWithPriceResponse
        {
            Dish = _mapper.Map<DishResponse>(dish),
            Price = dishPrices[dish.Id]
        }).ToList();

        return Result.Success(response);
    }
}