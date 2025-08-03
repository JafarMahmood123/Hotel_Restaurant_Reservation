using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;


namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveMealTypesFromRestaurant;

public class RemoveMealTypesFromRestaurantCommandHandler
    : ICommandHandler<RemoveMealTypesFromRestaurantCommand, Result<MealTypeResponse>>
{
    private readonly IGenericRepository<RestaurantMealType> _restaurantMealTypeRepository;
    private readonly IGenericRepository<MealType> _mealTypeRepository;
    private readonly IMapper _mapper;

    public RemoveMealTypesFromRestaurantCommandHandler(
        IGenericRepository<RestaurantMealType> restaurantMealTypeRepository, IGenericRepository<MealType> mealTypeRepository,
        IMapper mapper)
    {
        _restaurantMealTypeRepository = restaurantMealTypeRepository;
        _mealTypeRepository = mealTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<MealTypeResponse>> Handle(
        RemoveMealTypesFromRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var mealTypeId = request.MealTypeId;

        var mealType = await _mealTypeRepository.GetByIdAsync(mealTypeId);
        if (mealType == null)
        {
            return Result.Failure<MealTypeResponse>(
                DomainErrors.MealType.NotFound(mealTypeId));
        }

        // Get all existing associations
        var restaurantMealTypes = await _restaurantMealTypeRepository
            .GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId && mealTypeId == x.MealTypeId);

        if (restaurantMealTypes == null)
        {
            return Result.Failure<MealTypeResponse>(
                DomainErrors.Restaurant.NoMealTypesToRemove);
        }

        // Remove associations
        await _restaurantMealTypeRepository.RemoveAsync(restaurantMealTypes.Id);
        await _restaurantMealTypeRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = _mapper.Map<MealTypeResponse>(mealType);
        return Result.Success(response);
    }
}