using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.UpdateAmenity;

public class UpdateAmenityCommandHandler : ICommandHandler<UpdateAmenityCommand, Result<AmenityResponse>>
{
    private readonly IGenericRepository<Amenity> _amenityRepository;
    private readonly IMapper _mapper;

    public UpdateAmenityCommandHandler(IGenericRepository<Amenity> amenityRepository, IMapper mapper)
    {
        _amenityRepository = amenityRepository;
        _mapper = mapper;
    }

    public async Task<Result<AmenityResponse>> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
    {
        var amenity = await _amenityRepository.GetByIdAsync(request.Id);

        if (amenity is null)
        {
            return Result.Failure<AmenityResponse>(DomainErrors.Amenity.NotFound(request.Id));
        }

        _mapper.Map(request.UpdateAmenityRequest, amenity);

        amenity = await _amenityRepository.UpdateAsync(request.Id, amenity);
        await _amenityRepository.SaveChangesAsync();

        var amenityResponse = _mapper.Map<AmenityResponse>(amenity);

        return Result.Success(amenityResponse);
    }
}