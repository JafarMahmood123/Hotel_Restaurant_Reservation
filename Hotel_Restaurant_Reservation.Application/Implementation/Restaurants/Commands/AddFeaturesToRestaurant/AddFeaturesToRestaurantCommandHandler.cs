using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddFeaturesToRestaurant;

public class AddFeaturesToRestaurantCommandHandler
    : ICommandHandler<AddFeaturesToRestaurantCommand, Result<List<FeatureResponse>>>
{
    private readonly IGenericRepository<Feature> _featureRepository;
    private readonly IGenericRepository<RestaurantFeature> _restaurantFeatureRepository;
    private readonly IMapper _mapper;

    public AddFeaturesToRestaurantCommandHandler(
        IGenericRepository<Feature> featureRepository,
        IGenericRepository<RestaurantFeature> restaurantFeatureRepository,
        IMapper mapper)
    {
        _featureRepository = featureRepository;
        _restaurantFeatureRepository = restaurantFeatureRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<FeatureResponse>>> Handle(
        AddFeaturesToRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var featureIds = request.Request.Ids;

        List<Feature> features = new();
        List<RestaurantFeature> restaurantFeatures = new();

        // Verify all features exist
        foreach (var featureId in featureIds)
        {
            var feature = await _featureRepository.GetByIdAsync(featureId);
            if (feature == null)
            {
                return Result.Failure<List<FeatureResponse>>(
                    DomainErrors.Feature.NotFound(featureId));
            }
            features.Add(feature);
        }

        // Create new restaurant-feature associations
        foreach (var featureId in featureIds)
        {
            var existingAssociation = await _restaurantFeatureRepository.GetFirstOrDefaultAsync(
                x => x.RestaurantId == restaurantId && x.FeatureId == featureId);

            if (existingAssociation == null)
            {
                var newAssociation = new RestaurantFeature
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = restaurantId,
                    FeatureId = featureId
                };
                restaurantFeatures.Add(newAssociation);
                await _restaurantFeatureRepository.AddAsync(newAssociation);
                await _restaurantFeatureRepository.SaveChangesAsync();
            }
        }

        // Map to response DTOs
        var response = _mapper.Map<List<FeatureResponse>>(features);

        return Result.Success(response);
    }
}