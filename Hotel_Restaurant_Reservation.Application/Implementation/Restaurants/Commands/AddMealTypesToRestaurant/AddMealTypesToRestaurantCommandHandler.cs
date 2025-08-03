using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddMealTypesToRestaurant;

public class AddMealTypesToRestaurantCommandHandler : ICommandHandler<AddMealTypesToRestaurantCommand, Result<MealTypeResponse>>
{
    private readonly IGenericRepository<MealType> _mealTypeRepository;
    private readonly IGenericRepository<RestaurantMealType> _restaurantMealTypeRepository;
    private readonly IMapper _mapper;

    public AddMealTypesToRestaurantCommandHandler(
        IGenericRepository<MealType> mealTypeRepository, IGenericRepository<RestaurantMealType> restaurantMealTypeRepository,
        IMapper mapper)
    {
        _mealTypeRepository = mealTypeRepository;
        _restaurantMealTypeRepository = restaurantMealTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<MealTypeResponse>> Handle(AddMealTypesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var mealTypeId = request.MealTypeId;

        var mealType = await _mealTypeRepository.GetByIdAsync(mealTypeId);

        if (mealType == null)
            return Result.Failure<MealTypeResponse>(DomainErrors.MealType.NotFound(mealTypeId));

        var existingAssociation = await _restaurantMealTypeRepository.GetFirstOrDefaultAsync(
                x => x.RestaurantId == restaurantId && x.MealTypeId == mealTypeId);

        if (existingAssociation != null)
        {
            
        }

        var newAssociation = new RestaurantMealType()
        {
            Id = Guid.NewGuid(),
            MealTypeId = mealTypeId,
            RestaurantId = restaurantId
        };

        await _restaurantMealTypeRepository.AddAsync(newAssociation);
        await _restaurantMealTypeRepository.SaveChangesAsync();

        // Map to response DTOs
        var mealTypeResponses = _mapper.Map<MealTypeResponse>(mealType);

        return Result.Success(mealTypeResponses);
    }
}