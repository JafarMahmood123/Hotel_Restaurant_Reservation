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
    private readonly IGenericRepository<CityLocalLocations> _cityLocalLocationsRepository;
    private readonly IMapper _mapper;

    public GetLocationByIdQueryHandler(IGenericRepository<Location> locationRepository,
        IGenericRepository<CityLocalLocations> cityLocalLocationsRepository,
        IMapper mapper)
    {
        _locationRepository = locationRepository;
        _cityLocalLocationsRepository = cityLocalLocationsRepository;
        _mapper = mapper;
    }

    public async Task<Result<LocationResponse>> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetByIdAsync(request.Id);

        if (location is null)
        {
            return Result.Failure<LocationResponse>(DomainErrors.Location.NotFound(request.Id));
        }

        var cityLocalLocaton = await _cityLocalLocationsRepository.GetByIdAsync(location.CityLocalLocationsId);

        var locationResponse = new LocationResponse()
        {
            Id = location.Id,
            CountryId = location.CountryId,
            CityId = cityLocalLocaton.CityId,
            LocalLocationId = cityLocalLocaton.LocalLocationId,
        };

        return Result.Success(locationResponse);
    }
}