using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.DeleteAmenity;

public class DeleteAmenityCommandHandler : ICommandHandler<DeleteAmenityCommand, Result>
{
    private readonly IGenericRepository<Amenity> _amenityRepository;

    public DeleteAmenityCommandHandler(IGenericRepository<Amenity> amenityRepository)
    {
        _amenityRepository = amenityRepository;
    }

    public async Task<Result> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
    {
        var amenity = await _amenityRepository.GetByIdAsync(request.Id);

        if (amenity is null)
        {
            return Result.Failure(DomainErrors.Amenity.NotFound(request.Id));
        }

        await _amenityRepository.RemoveAsync(request.Id);
        await _amenityRepository.SaveChangesAsync();

        return Result.Success();
    }
}