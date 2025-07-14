using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.DeleteLocalLocation;

public class DeleteLocalLocationCommandHandler : ICommandHandler<DeleteLocalLocationCommand, Result>
{
    private readonly IGenericRepository<LocalLocation> _localLocationRepository;

    public DeleteLocalLocationCommandHandler(IGenericRepository<LocalLocation> localLocationRepository)
    {
        _localLocationRepository = localLocationRepository;
    }

    public async Task<Result> Handle(DeleteLocalLocationCommand request, CancellationToken cancellationToken)
    {
        var localLocation = await _localLocationRepository.GetByIdAsync(request.Id);

        if (localLocation is null)
        {
            return Result.Failure(DomainErrors.LocalLocation.NotFound(request.Id));
        }

        await _localLocationRepository.RemoveAsync(request.Id);
        await _localLocationRepository.SaveChangesAsync();

        return Result.Success();
    }
}