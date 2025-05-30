using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.LocationDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;

public class AddRestaurantCommandHandler : ICommandHandler<AddRestaurantCommand, Result<RestaurantResponse>>
{
    private readonly IGenericRepository<Restaurant> _restaurantRepository;
    private readonly IGenericRepository<Location> _locationRepository;
    private readonly IMapper _mapper;

    public AddRestaurantCommandHandler(
        IGenericRepository<Restaurant> restaurantRepository,
        IGenericRepository<Location> locationRepository,
        IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<Result<RestaurantResponse>> Handle(
        AddRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        // Validate request
        if (request.AddRestaurantRequest == null)
            return Result.Failure<RestaurantResponse>(DomainErrors.Restaurant.InvalidRequest);

        // Handle location
        var locationResult = await HandleLocation(request.AddRestaurantRequest.addLocationRequest);
        if (locationResult.IsFailure)
            return Result.Failure<RestaurantResponse>(locationResult.Error);

        // Create restaurant
        var restaurant = _mapper.Map<Restaurant>(request.AddRestaurantRequest);
        restaurant.Location = locationResult.Value;
        restaurant.Id = Guid.NewGuid();

        // Validate restaurant
        var validationError = ValidateRestaurant(restaurant);
        if (validationError != null)
            return Result.Failure<RestaurantResponse>(validationError);

        // Save restaurant
        restaurant = await _restaurantRepository.AddAsync(restaurant);
        await _restaurantRepository.SaveChangesAsync();

        // Map to response
        var response = _mapper.Map<RestaurantResponse>(restaurant);
        response.MinPrice = 0; 
        response.MaxPrice = 0;
        //The min and max price should be set after the adding of the dishes and their prices.

        return Result.Success(response);
    }

    private async Task<Result<Location>> HandleLocation(AddLocationRequest locationRequest)
    {
        if (locationRequest == null)
            return Result.Failure<Location>(DomainErrors.Location.InvalidRequest);

        var existingLocation = await _locationRepository.GetFirstOrDefaultAsync(
            x => x.CountryId == locationRequest.CountryId &&
                 x.CityLocalLocationsId == locationRequest.CityLocalLocationsId);

        if (existingLocation != null)
            return Result.Success(existingLocation);

        var newLocation = _mapper.Map<Location>(locationRequest);
        newLocation.Id = Guid.NewGuid();

        newLocation = await _locationRepository.AddAsync(newLocation);
        await _locationRepository.SaveChangesAsync();

        return Result.Success(newLocation);
    }

    private Error? ValidateRestaurant(Restaurant restaurant)
    {
        if (restaurant.StarRating < 1 || restaurant.StarRating > 5)
            return DomainErrors.Restaurant.InvalidStarRating;

        if (restaurant.NumberOfTables <= 0)
            return DomainErrors.Restaurant.InvalidTableCount;

        return null;
    }
}