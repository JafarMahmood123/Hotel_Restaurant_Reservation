using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetFeaturesByRestaurantId;

public class GetFeaturesByRestaurantIdQueryHandler : IQueryHandler<GetFeaturesByRestaurantIdQuery, Result<IEnumerable<GetFeaturesByRestaurantIdResponse>>>
{
    private readonly IGenericRepository<RestaurantFeature> _restaurantFeaturesRepository;
    private readonly IMapper _mapper;

    public GetFeaturesByRestaurantIdQueryHandler(IGenericRepository<RestaurantFeature> restaurantFeaturesRepository,
        IMapper mapper)
    {
        _restaurantFeaturesRepository = restaurantFeaturesRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<GetFeaturesByRestaurantIdResponse>>> Handle(GetFeaturesByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var restaurantFeatures = await _restaurantFeaturesRepository.Where(x => x.RestaurantId == request.RestaurantId)
            .Include(x => x.Feature).ToListAsync();

        var result = _mapper.Map<List<GetFeaturesByRestaurantIdResponse>>(restaurantFeatures);

        return Result.Success((IEnumerable<GetFeaturesByRestaurantIdResponse>)result);
    }
}
