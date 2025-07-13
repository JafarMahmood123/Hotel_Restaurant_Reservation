using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCuisinesFromRestaurant;

public class RemoveCuisinesFromRestaurantCommandHandler
    : ICommandHandler<RemoveCuisinesFromRestaurantCommand, Result<List<CuisineResponse>>>
{
    private readonly IGenericRepository<RestaurantCuisine> _restaurantCuisineRepository;
    private readonly IGenericRepository<Cuisine> _cuisineRepository;
    private readonly IMapper _mapper;

    public RemoveCuisinesFromRestaurantCommandHandler(
        IGenericRepository<RestaurantCuisine> restaurantCuisineRepository,
        IGenericRepository<Cuisine> cuisineRepository,
        IMapper mapper)
    {
        _restaurantCuisineRepository = restaurantCuisineRepository;
        _cuisineRepository = cuisineRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CuisineResponse>>> Handle(
        RemoveCuisinesFromRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var cuisineIds = request.RemoveCuisineFromRestaurantRequest.Ids;

        // Verify all cuisines exist
        var cuisines = new List<Cuisine>();
        foreach (var cuisineId in cuisineIds)
        {
            var cuisine = await _cuisineRepository.GetByIdAsync(cuisineId);
            if (cuisine == null)
            {
                return Result.Failure<List<CuisineResponse>>(
                    DomainErrors.Cuisine.NotExistCuisine(cuisineId));
            }
            cuisines.Add(cuisine);
        }

        // Get all existing associations
        var restaurantCuisines = await _restaurantCuisineRepository
            .Where(x => x.RestaurantId == restaurantId && cuisineIds.Contains(x.CuisineId))
            .ToListAsync();

        if (!restaurantCuisines.Any())
        {
            return Result.Failure<List<CuisineResponse>>(
                DomainErrors.Restaurant.NoCuisinesToRemove);
        }

        // Remove associations
        _restaurantCuisineRepository.RemoveRange(restaurantCuisines);
        await _restaurantCuisineRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = _mapper.Map<List<CuisineResponse>>(cuisines);
        return Result.Success(response);
    }
}