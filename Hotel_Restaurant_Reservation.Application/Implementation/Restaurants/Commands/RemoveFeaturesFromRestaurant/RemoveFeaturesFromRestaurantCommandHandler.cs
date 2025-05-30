using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveFeaturesFromRestaurant;

public class RemoveFeaturesFromRestaurantCommandHandler
    : ICommandHandler<RemoveFeaturesFromRestaurantCommand, Result<List<FeatureResponse>>>
{
    private readonly IGenericRepository<RestaurantFeature> _restaurantFeatureRepository;
    private readonly IGenericRepository<Feature> _featureRepository;
    private readonly IMapper _mapper;

    public RemoveFeaturesFromRestaurantCommandHandler(
        IGenericRepository<RestaurantFeature> restaurantFeatureRepository,
        IGenericRepository<Feature> featureRepository,
        IMapper mapper)
    {
        _restaurantFeatureRepository = restaurantFeatureRepository;
        _featureRepository = featureRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<FeatureResponse>>> Handle(
        RemoveFeaturesFromRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var featureIds = request.RemoveFeaturesFromRestaurantRequest.Ids;

        // Verify all features exist
        var features = new List<Feature>();
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

        // Get all existing associations
        var restaurantFeatures = await _restaurantFeatureRepository
            .Where(x => x.RestaurantId == restaurantId && featureIds.Contains(x.FeatureId))
            .ToListAsync();

        if (!restaurantFeatures.Any())
        {
            return Result.Failure<List<FeatureResponse>>(
                DomainErrors.Restaurant.NoFeaturesToRemove);
        }

        // Remove associations
        _restaurantFeatureRepository.RemoveRange(restaurantFeatures);
        await _restaurantFeatureRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = _mapper.Map<List<FeatureResponse>>(features);
        return Result.Success(response);
    }
}