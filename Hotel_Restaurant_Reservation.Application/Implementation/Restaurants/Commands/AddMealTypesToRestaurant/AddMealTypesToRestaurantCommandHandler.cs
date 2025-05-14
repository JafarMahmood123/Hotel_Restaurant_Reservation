using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddMealTypesToRestaurant;

public class AddMealTypesToRestaurantCommandHandler : ICommandHandler<AddMealTypesToRestaurantCommand, IEnumerable<MealType>>
{
    private readonly IGenericRepository<MealType> mealTypeRepository;
    private readonly IGenericRepository<RestaurantMealType> restaurantMealTypeRepository;

    public AddMealTypesToRestaurantCommandHandler(IGenericRepository<MealType> mealTypeRepository,
        IGenericRepository<RestaurantMealType> restaurantMealTypeRepository)
    {
        this.mealTypeRepository = mealTypeRepository;
        this.restaurantMealTypeRepository = restaurantMealTypeRepository;
    }

    public async Task<IEnumerable<MealType>> Handle(AddMealTypesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        var mealTypeIds = request.MealTypeIds;

        List<RestaurantMealType> restaurantMealTypes = new List<RestaurantMealType>();

        foreach (var mealTypeId in mealTypeIds)
        {
            var restaurantMealType = await restaurantMealTypeRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.MealTypeId == mealTypeId);

            if (restaurantMealType == null)
            {

                restaurantMealType = new RestaurantMealType()
                {
                    Id = Guid.NewGuid(),
                    MealTypeId = mealTypeId,
                    RestaurantId = restaurantId
                };

                restaurantMealTypes.Add(restaurantMealType);
            }

        }

        await restaurantMealTypeRepository.AddRangeAsync(restaurantMealTypes);

        await restaurantMealTypeRepository.SaveChangesAsync();


        List<MealType> features = new List<MealType>();

        foreach (var mealTypeId in mealTypeIds)
        {
            features.Add(await mealTypeRepository.GetByIdAsync(mealTypeId));
        }

        return features;
    }
}
