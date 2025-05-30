using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler
    : ICommandHandler<UpdateRestaurantCommand, Result<RestaurantResponse>>
{
    private readonly IGenericRepository<Restaurant> _restaurantRepository;
    private readonly IMapper _mapper;

    public UpdateRestaurantCommandHandler(
        IGenericRepository<Restaurant> restaurantRepository,
        IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    public async Task<Result<RestaurantResponse>> Handle(
        UpdateRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.Id;
        var updateRequest = request.RestaurantUpdateRequest;

        // Get existing restaurant
        var existingRestaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
        if (existingRestaurant == null)
        {
            return Result.Failure<RestaurantResponse>(
                DomainErrors.Restaurant.NotFound(restaurantId));
        }

        // Validate request
        var validationError = ValidateRestaurantUpdate(updateRequest);
        if (validationError != null)
        {
            return Result.Failure<RestaurantResponse>(validationError);
        }

        // Update properties
        _mapper.Map(updateRequest, existingRestaurant);

        // Save changes
        var updatedRestaurant = await _restaurantRepository.UpdateAsync(restaurantId, existingRestaurant);
        await _restaurantRepository.SaveChangesAsync();

        // Map to response DTO
        var response = _mapper.Map<RestaurantResponse>(updatedRestaurant);
        return Result.Success(response);
    }

    private Error? ValidateRestaurantUpdate(RestaurantUpdateRequest request)
    {
        //if (request.StarRating < 1 || request.StarRating > 5)
        //{
        //    return DomainErrors.Restaurant.InvalidStarRating;
        //}

        //if (request.NumberOfTables <= 0)
        //{
        //    return DomainErrors.Restaurant.InvalidTableCount;
        //}

        //if (request.MinPrice < 0 || request.MaxPrice < 0 || request.MinPrice > request.MaxPrice)
        //{
        //    return DomainErrors.Restaurant.InvalidPriceRange;
        //}

        //The rating is the average rating that should be calculate alone.

        return null;
    }
}