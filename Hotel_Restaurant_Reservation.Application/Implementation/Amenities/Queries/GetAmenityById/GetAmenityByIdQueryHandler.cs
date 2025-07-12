using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries.GetAmenityById;

public class GetAmenityByIdQueryHandler : IQueryHandler<GetAmenityByIdQuery, Result<AmenityResponse>>
{
    private readonly IGenericRepository<Amenity> _amenityRepository;
    private readonly IMapper _mapper;

    public GetAmenityByIdQueryHandler(IGenericRepository<Amenity> amenityRepository, IMapper mapper)
    {
        _amenityRepository = amenityRepository;
        _mapper = mapper;
    }

    public async Task<Result<AmenityResponse>> Handle(GetAmenityByIdQuery request, CancellationToken cancellationToken)
    {
        var amenity = await _amenityRepository.GetByIdAsync(request.Id);

        if (amenity is null)
        {
            return Result.Failure<AmenityResponse>(DomainErrors.Amenity.NotFound(request.Id));
        }

        var amenityResponse = _mapper.Map<AmenityResponse>(amenity);
        return Result.Success(amenityResponse);
    }
}