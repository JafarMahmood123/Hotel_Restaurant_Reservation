using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Features.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddFeaturesToRestaurant;

public class AddFeaturesToRestaurantCommandHandler
    : ICommandHandler<AddFeaturesToRestaurantCommand, Result<FeatureResponse>>
{
    private readonly IGenericRepository<Feature> _featureRepository;
    private readonly IGenericRepository<RestaurantFeature> _restaurantFeatureRepository;
    private readonly IMapper _mapper;

    public AddFeaturesToRestaurantCommandHandler(IGenericRepository<Feature> featureRepository, IGenericRepository<RestaurantFeature> restaurantFeatureRepository,
        IMapper mapper)
    {
        _featureRepository = featureRepository;
        _restaurantFeatureRepository = restaurantFeatureRepository;
        _mapper = mapper;
    }

    public async Task<Result<FeatureResponse>> Handle(
        AddFeaturesToRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var featureId = request.FeatureId;

        var feature = await _featureRepository.GetByIdAsync(featureId);
        if (feature == null)
        {
            return Result.Failure<FeatureResponse>(
                DomainErrors.Feature.NotFound(featureId));
        }

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
            await _restaurantFeatureRepository.AddAsync(newAssociation);
            await _restaurantFeatureRepository.SaveChangesAsync();
        }

        var response = _mapper.Map<FeatureResponse>(feature);

        return Result.Success(response);
    }
}