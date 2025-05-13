using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddFeaturesToRestaurant;

public class AddFeaturesToRestaurantCommandHandler : ICommandHandler<AddFeaturesToRestaurantCommand, IEnumerable<Feature>>
{
    private readonly IGenericRepository<Feature> featureRepository;
    private readonly IGenericRepository<RestaurantFeature> restaurantFeatureRepository;

    public AddFeaturesToRestaurantCommandHandler(IGenericRepository<Feature> featureRepository,
        IGenericRepository<RestaurantFeature> restaurantFeatureRepository)
    {
        this.featureRepository = featureRepository;
        this.restaurantFeatureRepository = restaurantFeatureRepository;
    }

    public async Task<IEnumerable<Feature>> Handle(AddFeaturesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        var featureIds = request.FeatureIds;

        List<RestaurantFeature> restaurantFeatures = new List<RestaurantFeature>();

        foreach (var featureId in featureIds)
        {
            var restaurantFeature = await restaurantFeatureRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.FeatureId == featureId);

            if (restaurantFeature == null)
            {

                restaurantFeature = new RestaurantFeature()
                {
                    Id = Guid.NewGuid(),
                    FeatureId = featureId,
                    RestaurantId = restaurantId
                };

                restaurantFeatures.Add(restaurantFeature);
            }

        }

        await restaurantFeatureRepository.AddRangeAsync(restaurantFeatures);

        await restaurantFeatureRepository.SaveChangesAsync();


        List<Feature> features = new List<Feature>();

        foreach (var featureId in featureIds)
        {
            features.Add(await featureRepository.GetByIdAsync(featureId));
        }

        return features;
    }
}
