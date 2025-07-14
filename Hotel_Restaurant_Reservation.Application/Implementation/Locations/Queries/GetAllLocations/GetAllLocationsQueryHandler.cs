using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetAllLocations;

public class GetAllLocationsQueryHandler : IQueryHandler<GetAllLocationsQuery, Result<IEnumerable<LocationResponse>>>
{
    private readonly IGenericRepository<Location> _locationRepository;
    private readonly IMapper _mapper;

    public GetAllLocationsQueryHandler(IGenericRepository<Location> locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<LocationResponse>>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        var locations = await _locationRepository.GetAllAsync();
        var locationResponses = _mapper.Map<IEnumerable<LocationResponse>>(locations);
        return Result.Success(locationResponses);
    }
}