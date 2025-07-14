using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetLocationById;

public class GetLocationByIdQueryHandler : IQueryHandler<GetLocationByIdQuery, Result<LocationResponse>>
{
    private readonly IGenericRepository<Location> _locationRepository;
    private readonly IMapper _mapper;

    public GetLocationByIdQueryHandler(IGenericRepository<Location> locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<Result<LocationResponse>> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetByIdAsync(request.Id);

        if (location is null)
        {
            return Result.Failure<LocationResponse>(DomainErrors.Location.NotFound(request.Id));
        }

        var locationResponse = _mapper.Map<LocationResponse>(location);
        return Result.Success(locationResponse);
    }
}