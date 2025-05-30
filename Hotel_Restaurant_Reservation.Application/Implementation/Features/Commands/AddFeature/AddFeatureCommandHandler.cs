using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Features.Commands.AddFeature;

public class AddFeatureCommandHandler : ICommandHandler<AddFeatureCommand, Result<FeatureResponse>>
{
    private readonly IGenericRepository<Feature> _featureRepository;
    private readonly IMapper _mapper;

    public AddFeatureCommandHandler(IGenericRepository<Feature> featureRepository, IMapper mapper)
    {
        _featureRepository = featureRepository;
        this._mapper = mapper;
    }

    public async Task<Result<FeatureResponse>> Handle(AddFeatureCommand request, CancellationToken cancellationToken)
    {
        var feature = _mapper.Map<Feature>(request.AddFeatureRequest);

        var existingFeature = await _featureRepository.GetFirstOrDefaultAsync(x => x.Name == feature.Name);

        if (existingFeature != null)
            return Result.Failure<FeatureResponse>(DomainErrors.Feature.ExistingFeature(feature.Name));

        feature.Id = Guid.NewGuid();

        feature = await _featureRepository.AddAsync(feature);

        await _featureRepository.SaveChangesAsync();

        var featureResponse = _mapper.Map<FeatureResponse>(feature);

        return Result.Success(featureResponse);
    }
}
