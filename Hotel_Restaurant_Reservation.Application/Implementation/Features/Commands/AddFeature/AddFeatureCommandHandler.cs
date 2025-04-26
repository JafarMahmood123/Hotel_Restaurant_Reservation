using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Features.Commands.AddFeature;

public class AddFeatureCommandHandler : ICommandHandler<AddFeatureCommand, Feature>
{
    private readonly IGenericRepository<Feature> _genericRepository;

    public AddFeatureCommandHandler(IGenericRepository<Feature> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Feature> Handle(AddFeatureCommand request, CancellationToken cancellationToken)
    {
        Feature feature = request.Feature;

        feature = await _genericRepository.AddAsync(feature);

        await _genericRepository.SaveChangesAsync();

        return feature;
    }
}
