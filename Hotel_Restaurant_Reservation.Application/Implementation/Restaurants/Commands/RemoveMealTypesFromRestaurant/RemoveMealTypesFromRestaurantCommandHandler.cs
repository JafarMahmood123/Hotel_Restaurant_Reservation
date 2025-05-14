using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveMealTypesFromRestaurant;

public class RemoveMealTypesFromRestaurantCommandHandler : ICommandHandler<RemoveMealTypesFromRestaurantCommand, IEnumerable<MealType>>
{
    private readonly IGenericRepository<RestaurantMealType> restaurantMealTypeRepository;
    private readonly IGenericRepository<MealType> mealTypeRepository;

    public RemoveMealTypesFromRestaurantCommandHandler(IGenericRepository<RestaurantMealType> restaurantMealTypeRepository,
        IGenericRepository<MealType> mealTypeRepository)
    {
        this.restaurantMealTypeRepository = restaurantMealTypeRepository;
        this.mealTypeRepository = mealTypeRepository;
    }

    public async Task<IEnumerable<MealType>> Handle(RemoveMealTypesFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var mealTypeIds = request.MealTypeIds;


        List<RestaurantMealType> restaurantFeatures = new List<RestaurantMealType>();
        foreach (var mealTypeId in mealTypeIds)
        {
            var restaurantFeature = await restaurantMealTypeRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.MealTypeId == mealTypeId);

            restaurantFeatures.Add(restaurantFeature);
        }

        restaurantMealTypeRepository.RemoveRange(restaurantFeatures);

        await restaurantMealTypeRepository.SaveChangesAsync();

        List<MealType> mealTypes = new List<MealType>();

        foreach (var mealTypeId in mealTypeIds)
        {
            mealTypes.Add(await mealTypeRepository.GetByIdAsync(mealTypeId));
        }

        return mealTypes;
    }
}
