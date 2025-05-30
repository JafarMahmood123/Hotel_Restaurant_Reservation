using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.MealTypeDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Errors;
using Hotel_Restaurant_Reservation.Domain.Shared;
using MediatR;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddMealTypesToRestaurant;

public class AddMealTypesToRestaurantCommandHandler : ICommandHandler<AddMealTypesToRestaurantCommand, Result<List<MealTypeResponse>>>
{
    private readonly IGenericRepository<MealType> _mealTypeRepository;
    private readonly IGenericRepository<RestaurantMealType> _restaurantMealTypeRepository;
    private readonly IMapper _mapper;

    public AddMealTypesToRestaurantCommandHandler(
        IGenericRepository<MealType> mealTypeRepository,
        IGenericRepository<RestaurantMealType> restaurantMealTypeRepository,
        IMapper mapper)
    {
        _mealTypeRepository = mealTypeRepository;
        _restaurantMealTypeRepository = restaurantMealTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<MealTypeResponse>>> Handle(AddMealTypesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var mealTypeIds = request.AddMealTypeToRestaurantRequest.Ids;

        List<MealType> mealTypes = new List<MealType>();

        // Verify all meal types exist
        foreach (var mealTypeId in mealTypeIds)
        {
            var mealType = await _mealTypeRepository.GetByIdAsync(mealTypeId);

            if (mealType == null)
                return Result.Failure<List<MealTypeResponse>>(DomainErrors.MealType.NotExistMealType);

            mealTypes.Add(mealType);
        }

        // Add new restaurant-meal type associations
        List<RestaurantMealType> restaurantMealTypes = new List<RestaurantMealType>();

        foreach (var mealTypeId in mealTypeIds)
        {
            var existingAssociation = await _restaurantMealTypeRepository.GetFirstOrDefaultAsync(
                x => x.RestaurantId == restaurantId && x.MealTypeId == mealTypeId);

            if (existingAssociation == null)
            {
                var newAssociation = new RestaurantMealType()
                {
                    Id = Guid.NewGuid(),
                    MealTypeId = mealTypeId,
                    RestaurantId = restaurantId
                };

                restaurantMealTypes.Add(newAssociation);
                await _restaurantMealTypeRepository.AddAsync(newAssociation);
            }
        }

        await _restaurantMealTypeRepository.SaveChangesAsync();

        // Map to response DTOs
        var mealTypeResponses = _mapper.Map<List<MealTypeResponse>>(mealTypes);

        return Result.Success(mealTypeResponses);
    }
}