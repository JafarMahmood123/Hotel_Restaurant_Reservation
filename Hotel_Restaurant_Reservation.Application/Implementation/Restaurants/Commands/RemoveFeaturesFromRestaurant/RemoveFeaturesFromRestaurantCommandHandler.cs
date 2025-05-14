using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveFeaturesFromRestaurant;

public class RemoveFeaturesFromRestaurantCommandHandler : ICommandHandler<RemoveFeaturesFromRestaurantCommand, IEnumerable<Feature>>
{
    private readonly IGenericRepository<RestaurantFeature> restaurantFeatureRepository;
    private readonly IGenericRepository<Feature> featureRepository;

    public RemoveFeaturesFromRestaurantCommandHandler(IGenericRepository<RestaurantFeature> restaurantFeatureRepository,
        IGenericRepository<Feature> featureRepository)
    {
        this.restaurantFeatureRepository = restaurantFeatureRepository;
        this.featureRepository = featureRepository;
    }

    public async Task<IEnumerable<Feature>> Handle(RemoveFeaturesFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var featureIds = request.FeatureIds;


        List<RestaurantFeature> restaurantFeatures = new List<RestaurantFeature>();
        foreach (var faetureId in featureIds)
        {
            var restaurantFeature = await restaurantFeatureRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.FeatureId == faetureId);

            restaurantFeatures.Add(restaurantFeature);
        }

        restaurantFeatureRepository.RemoveRange(restaurantFeatures);

        await restaurantFeatureRepository.SaveChangesAsync();

        List<Feature> features = new List<Feature>();

        foreach (var featureId in featureIds)
        {
            features.Add(await featureRepository.GetByIdAsync(featureId));
        }

        return features;
    }
}
