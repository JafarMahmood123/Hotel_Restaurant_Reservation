using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : ICommandHandler<DeleteRestaurantCommand, Result<RestaurantResponse>>
{
    private readonly IGenericRepository<Restaurant> _restaurantRepository;
    private readonly IMapper _mapper;

    public DeleteRestaurantCommandHandler(
        IGenericRepository<Restaurant> restaurantRepository,
        IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    public async Task<Result<RestaurantResponse>> Handle(
        DeleteRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.Id;

        // Check if restaurant exists
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
        if (restaurant == null)
        {
            return Result.Failure<RestaurantResponse>(DomainErrors.Restaurant.NotFound(request.Id));
        }

        restaurant = await _restaurantRepository.RemoveAsync(restaurantId);

        var restaurantResponse = _mapper.Map<RestaurantResponse>(restaurant);

        return Result.Success(restaurantResponse);
    }
}