using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveMealTypesFromRestaurant;

public class RemoveMealTypesFromRestaurantCommandHandler
    : ICommandHandler<RemoveMealTypesFromRestaurantCommand, Result<List<MealTypeResponse>>>
{
    private readonly IGenericRepository<RestaurantMealType> _restaurantMealTypeRepository;
    private readonly IGenericRepository<MealType> _mealTypeRepository;
    private readonly IMapper _mapper;

    public RemoveMealTypesFromRestaurantCommandHandler(
        IGenericRepository<RestaurantMealType> restaurantMealTypeRepository,
        IGenericRepository<MealType> mealTypeRepository,
        IMapper mapper)
    {
        _restaurantMealTypeRepository = restaurantMealTypeRepository;
        _mealTypeRepository = mealTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<MealTypeResponse>>> Handle(
        RemoveMealTypesFromRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var mealTypeIds = request.RemoveMealTypesFromRestaurantRequest.Ids;

        // Verify all meal types exist
        var mealTypes = new List<MealType>();
        foreach (var mealTypeId in mealTypeIds)
        {
            var mealType = await _mealTypeRepository.GetByIdAsync(mealTypeId);
            if (mealType == null)
            {
                return Result.Failure<List<MealTypeResponse>>(
                    DomainErrors.MealType.NotFound(mealTypeId));
            }
            mealTypes.Add(mealType);
        }

        // Get all existing associations
        var restaurantMealTypes = await _restaurantMealTypeRepository
            .Where(x => x.RestaurantId == restaurantId && mealTypeIds.Contains(x.MealTypeId))
            .ToListAsync();

        if (!restaurantMealTypes.Any())
        {
            return Result.Failure<List<MealTypeResponse>>(
                DomainErrors.Restaurant.NoMealTypesToRemove);
        }

        // Remove associations
        _restaurantMealTypeRepository.RemoveRange(restaurantMealTypes);
        await _restaurantMealTypeRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = _mapper.Map<List<MealTypeResponse>>(mealTypes);
        return Result.Success(response);
    }
}