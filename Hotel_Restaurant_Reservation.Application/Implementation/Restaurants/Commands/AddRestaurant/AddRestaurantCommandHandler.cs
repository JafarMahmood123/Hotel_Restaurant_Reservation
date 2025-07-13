using AutoMapper;
using FluentValidation;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Hotel_Restaurant_Reservation.Domain.Shared;

public class AddRestaurantCommandHandler : ICommandHandler<AddRestaurantCommand, Result<RestaurantResponse>>
{
    private readonly IGenericRepository<Restaurant> _restaurantRepository;
    private readonly IGenericRepository<Location> _locationRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AddRestaurantRequest> _validator;

    public AddRestaurantCommandHandler(
        IGenericRepository<Restaurant> restaurantRepository,
        IGenericRepository<Location> locationRepository,
        IMapper mapper,
        IValidator<AddRestaurantRequest> validator)
    {
        _restaurantRepository = restaurantRepository;
        _locationRepository = locationRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<RestaurantResponse>> Handle(
        AddRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        // Validate request
        if (request.AddRestaurantRequest == null)
            return Result.Failure<RestaurantResponse>(DomainErrors.Restaurant.InvalidRequest);

        var validationResult = await _validator.ValidateAsync(request.AddRestaurantRequest, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Failure<RestaurantResponse>(new Error(
                "Validation.Error",
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))));
        }

        // Handle location
        var locationResult = await HandleLocation(request.AddRestaurantRequest.LocationId);
        if (locationResult.IsFailure)
            return Result.Failure<RestaurantResponse>(locationResult.Error);

        // Create restaurant with all required properties
        var restaurant = _mapper.Map<Restaurant>(request.AddRestaurantRequest);
        restaurant.Id = Guid.NewGuid();
        restaurant.Location = locationResult.Value;
        restaurant.MinPrice = 0;
        restaurant.MaxPrice = 0;
        restaurant.PriceLevel = RestaurantPriceLevel.NotSet;

        // Initialize rating properties
        restaurant.StarRating = 0;          // Will be calculated when reviews are added
        //restaurant.ServiceRating = 0;       // Will be calculated when reviews are added
        //restaurant.FoodRating = 0;          // Will be calculated when reviews are added
        //restaurant.ReviewCount = 0;         // No reviews initially
        //restaurant.IsActive = true;         // New restaurants are active by default
        //restaurant.CreatedDate = DateTime.UtcNow;
        //restaurant.LastModifiedDate = DateTime.UtcNow;

        // Save restaurant
        restaurant = await _restaurantRepository.AddAsync(restaurant);
        await _restaurantRepository.SaveChangesAsync();

        // Map to response
        var response = _mapper.Map<RestaurantResponse>(restaurant);

        return Result.Success(response);
    }

    private async Task<Result<Location>> HandleLocation(Guid locationId)
    {
        var existingLocation = await _locationRepository.GetFirstOrDefaultAsync(
            x => x.Id == locationId);

        if (existingLocation != null)
            return Result.Success(existingLocation);

        return Result.Failure<Location>(DomainErrors.Location.NotFound(locationId));
    }
}