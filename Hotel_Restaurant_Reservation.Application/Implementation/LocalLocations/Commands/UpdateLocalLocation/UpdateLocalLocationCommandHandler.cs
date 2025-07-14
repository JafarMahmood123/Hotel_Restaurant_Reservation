using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.UpdateLocalLocation;

public class UpdateLocalLocationCommandHandler : ICommandHandler<UpdateLocalLocationCommand, Result<LocalLocationResponse>>
{
    private readonly IGenericRepository<LocalLocation> _localLocationRepository;
    private readonly IMapper _mapper;

    public UpdateLocalLocationCommandHandler(IGenericRepository<LocalLocation> localLocationRepository, IMapper mapper)
    {
        _localLocationRepository = localLocationRepository;
        _mapper = mapper;
    }

    public async Task<Result<LocalLocationResponse>> Handle(UpdateLocalLocationCommand request, CancellationToken cancellationToken)
    {
        var localLocation = await _localLocationRepository.GetByIdAsync(request.Id);

        if (localLocation is null)
        {
            return Result.Failure<LocalLocationResponse>(DomainErrors.LocalLocation.NotFound(request.Id));
        }

        if (localLocation.Name == request.UpdateLocalLocationRequest.Name)
        {
            return Result.Failure<LocalLocationResponse>(DomainErrors.LocalLocation.SameName);
        }

        var existingLocation = await _localLocationRepository.GetFirstOrDefaultAsync(
            x => x.Name == request.UpdateLocalLocationRequest.Name && x.Id != request.Id);
        if (existingLocation != null)
        {
            return Result.Failure<LocalLocationResponse>(DomainErrors.LocalLocation.ExistingLocalLocation(request.UpdateLocalLocationRequest.Name));
        }

        _mapper.Map(request.UpdateLocalLocationRequest, localLocation);

        await _localLocationRepository.UpdateAsync(request.Id, localLocation);
        await _localLocationRepository.SaveChangesAsync();

        var localLocationResponse = _mapper.Map<LocalLocationResponse>(localLocation);
        return Result.Success(localLocationResponse);
    }
}