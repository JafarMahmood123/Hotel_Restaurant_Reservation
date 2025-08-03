using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Features.Queries.GetAllFeatures;

public class GetAllFeaturesQueryHandler : IQueryHandler<GetAllFeaturesQuery, Result<IEnumerable<FeatureResponse>>>
{
    private readonly IGenericRepository<Feature> _featureRepository;
    private readonly IMapper _mapper;

    public GetAllFeaturesQueryHandler(IGenericRepository<Feature> featureRepository, IMapper mapper)
    {
        _featureRepository = featureRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<FeatureResponse>>> Handle(GetAllFeaturesQuery request, CancellationToken cancellationToken)
    {
        var features = await _featureRepository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<FeatureResponse>>(features);

        return Result.Success(response);
    }
}
