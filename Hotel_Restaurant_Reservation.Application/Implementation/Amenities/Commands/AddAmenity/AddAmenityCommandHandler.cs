using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.AddAmenity;

public class AddAmenityCommandHandler : ICommandHandler<AddAmenityCommand, Result<AmenityResponse>>
{
    private readonly IGenericRepository<Amenity> _amenityRepository;
    private readonly IMapper _mapper;

    public AddAmenityCommandHandler(IGenericRepository<Amenity> amenityRepository, IMapper mapper)
    {
        _amenityRepository = amenityRepository;
        _mapper = mapper;
    }

    public async Task<Result<AmenityResponse>> Handle(AddAmenityCommand request, CancellationToken cancellationToken)
    {
        var amenity = _mapper.Map<Amenity>(request.AddAmenityRequest);

        var existingAmenity = await _amenityRepository.GetFirstOrDefaultAsync(x => x.Name == amenity.Name);

        if (existingAmenity != null)
            return Result.Failure<AmenityResponse>(DomainErrors.Amenity.ExistingAmenity(amenity.Name));

        amenity.Id = Guid.NewGuid();

        amenity = await _amenityRepository.AddAsync(amenity);

        await _amenityRepository.SaveChangesAsync();

        var amenityResponse = _mapper.Map<AmenityResponse>(amenity);

        return Result.Success(amenityResponse);
    }
}