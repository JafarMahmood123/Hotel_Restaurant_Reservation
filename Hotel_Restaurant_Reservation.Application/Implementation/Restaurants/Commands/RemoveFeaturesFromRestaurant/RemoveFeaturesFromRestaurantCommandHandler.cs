using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Features.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveFeaturesFromRestaurant;

public class RemoveFeaturesFromRestaurantCommandHandler
    : ICommandHandler<RemoveFeaturesFromRestaurantCommand, Result<FeatureResponse>>
{
    private readonly IGenericRepository<RestaurantFeature> _restaurantFeatureRepository;
    private readonly IGenericRepository<Feature> _featureRepository;
    private readonly IMapper _mapper;

    public RemoveFeaturesFromRestaurantCommandHandler(
        IGenericRepository<RestaurantFeature> restaurantFeatureRepository, IGenericRepository<Feature> featureRepository, IMapper mapper)
    {
        _restaurantFeatureRepository = restaurantFeatureRepository;
        _featureRepository = featureRepository;
        _mapper = mapper;
    }

    public async Task<Result<FeatureResponse>> Handle(RemoveFeaturesFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var featureId = request.FeatureId;

        var feature = await _featureRepository.GetByIdAsync(featureId);
        if (feature == null)
        {
            return Result.Failure<FeatureResponse>(
                DomainErrors.Feature.NotFound(featureId));
        }

        var restaurantFeature = await _restaurantFeatureRepository
            .GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId && featureId == x.FeatureId);

        if (restaurantFeature == null)
        {
            return Result.Failure<FeatureResponse>(
                DomainErrors.Restaurant.NoFeaturesToRemove);
        }

        await _restaurantFeatureRepository.RemoveAsync(restaurantFeature.Id);
        await _restaurantFeatureRepository.SaveChangesAsync();

        var response = _mapper.Map<FeatureResponse>(feature);
        return Result.Success(response);
    }
}