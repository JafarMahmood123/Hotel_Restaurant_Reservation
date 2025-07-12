using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries.GetAllAmenities;

public class GetAllAmenitiesQueryHandler : IQueryHandler<GetAllAmenitiesQuery, Result<IEnumerable<AmenityResponse>>>
{
    private readonly IGenericRepository<Amenity> _amenityRepository;
    private readonly IMapper _mapper;

    public GetAllAmenitiesQueryHandler(IGenericRepository<Amenity> amenityRepository, IMapper mapper)
    {
        _amenityRepository = amenityRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<AmenityResponse>>> Handle(GetAllAmenitiesQuery request, CancellationToken cancellationToken)
    {
        var amenities = await _amenityRepository.GetAllAsync();
        var amenityResponses = _mapper.Map<IEnumerable<AmenityResponse>>(amenities);
        return Result.Success(amenityResponses);
    }
}