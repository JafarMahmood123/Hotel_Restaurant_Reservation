using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.DeleteLocation;

public class DeleteLocationCommandHandler : ICommandHandler<DeleteLocationCommand, Result>
{
    private readonly IGenericRepository<Location> _locationRepository;

    public DeleteLocationCommandHandler(IGenericRepository<Location> locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Result> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetByIdAsync(request.Id);

        if (location is null)
        {
            return Result.Failure(DomainErrors.Location.NotFound(request.Id));
        }

        await _locationRepository.RemoveAsync(request.Id);
        await _locationRepository.SaveChangesAsync();

        return Result.Success();
    }
}