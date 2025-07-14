using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.UpdateLocation;

public class UpdateLocationCommandHandler : ICommandHandler<UpdateLocationCommand, Result<LocationResponse>>
{
    private readonly IGenericRepository<Location> _locationRepository;
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IGenericRepository<CityLocalLocations> _cityLocalLocationsRepository;
    private readonly IMapper _mapper;

    public UpdateLocationCommandHandler(
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

    public async Task<Result<LocationResponse>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetByIdAsync(request.Id);
        if (location is null)
        {
            return Result.Failure<LocationResponse>(DomainErrors.Location.NotFound(request.Id));
        }

        if (location.CountryId == request.UpdateLocationRequest.CountryId &&
            location.CityLocalLocationsId == request.UpdateLocationRequest.CityLocalLocationsId)
        {
            return Result.Failure<LocationResponse>(DomainErrors.Location.SameLocation);
        }

        if (await _countryRepository.GetByIdAsync(request.UpdateLocationRequest.CountryId) is null)
        {
            return Result.Failure<LocationResponse>(DomainErrors.Country.NotFound(request.UpdateLocationRequest.CountryId));
        }

        if (await _cityLocalLocationsRepository.GetByIdAsync(request.UpdateLocationRequest.CityLocalLocationsId) is null)
        {
            return Result.Failure<LocationResponse>(DomainErrors.CityLocalLocations.NotFound(request.UpdateLocationRequest.CityLocalLocationsId));
        }

        var existingLocation = await _locationRepository.GetFirstOrDefaultAsync(
            x => x.CountryId == request.UpdateLocationRequest.CountryId &&
                 x.CityLocalLocationsId == request.UpdateLocationRequest.CityLocalLocationsId);
        if (existingLocation != null)
        {
            return Result.Failure<LocationResponse>(DomainErrors.Location.ExistingLocation);
        }

        _mapper.Map(request.UpdateLocationRequest, location);

        await _locationRepository.UpdateAsync(request.Id, location);
        await _locationRepository.SaveChangesAsync();

        var locationResponse = _mapper.Map<LocationResponse>(location);
        return Result.Success(locationResponse);
    }
}