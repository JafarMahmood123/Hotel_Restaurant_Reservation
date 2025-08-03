using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCuisinesFromRestaurant;

public class RemoveCuisinesFromRestaurantCommandHandler
    : ICommandHandler<RemoveCuisinesFromRestaurantCommand, Result<CuisineResponse>>
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

    public async Task<Result<CuisineResponse>> Handle(RemoveCuisinesFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var cuisineId = request.CuisineId;

        var cuisine = await _cuisineRepository.GetByIdAsync(cuisineId);
        if (cuisine == null)
        {
            return Result.Failure<CuisineResponse>(
                DomainErrors.Cuisine.NotExistCuisine(cuisineId));
        }


        var restaurantCuisines = await _restaurantCuisineRepository
            .GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId && x.CuisineId == cuisineId);

        if (restaurantCuisines == null)
        {
            return Result.Failure<CuisineResponse>(
                DomainErrors.Restaurant.NoCuisinesToRemove);
        }

        // Remove associations
        await _restaurantCuisineRepository.RemoveAsync(restaurantCuisines.Id);
        await _restaurantCuisineRepository.SaveChangesAsync();

        var response = _mapper.Map<CuisineResponse>(cuisine);
        return Result.Success(response);
    }
}