using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocation;

public class AddLocationCommandHandler : ICommandHandler<AddLocationCommand, Result<LocationResponse>>
{
    private readonly IGenericRepository<Location> _locationRepository;
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IGenericRepository<CityLocalLocations> _cityLocalLocationsRepository;
    private readonly IMapper _mapper;

    public AddLocationCommandHandler(
        IGenericRepository<Location> locationRepository,
        IGenericRepository<Country> countryRepository,
        IGenericRepository<CityLocalLocations> cityLocalLocationsRepository,
        IMapper mapper)
    {
        _locationRepository = locationRepository;
        _countryRepository = countryRepository;
        _cityLocalLocationsRepository = cityLocalLocationsRepository;
        _mapper = mapper;
    }

    public async Task<Result<LocationResponse>> Handle(AddLocationCommand request, CancellationToken cancellationToken)
    {
        if (await _countryRepository.GetByIdAsync(request.AddLocationRequest.CountryId) is null)
        {
            return Result.Failure<LocationResponse>(DomainErrors.Country.NotFound(request.AddLocationRequest.CountryId));
        }

        if (await _cityLocalLocationsRepository.GetByIdAsync(request.AddLocationRequest.CityLocalLocationsId) is null)
        {
            return Result.Failure<LocationResponse>(DomainErrors.CityLocalLocations.NotFound(request.AddLocationRequest.CityLocalLocationsId));
        }

        var existingLocation = await _locationRepository.GetFirstOrDefaultAsync(
            x => x.CountryId == request.AddLocationRequest.CountryId &&
                 x.CityLocalLocationsId == request.AddLocationRequest.CityLocalLocationsId);

        if (existingLocation != null)
        {
            return Result.Failure<LocationResponse>(DomainErrors.Location.ExistingLocation);
        }

        var location = _mapper.Map<Location>(request.AddLocationRequest);
        location.Id = Guid.NewGuid();

        await _locationRepository.AddAsync(location);
        await _locationRepository.SaveChangesAsync();

        var locationResponse = _mapper.Map<LocationResponse>(location);
        return Result.Success(locationResponse);
    }
}