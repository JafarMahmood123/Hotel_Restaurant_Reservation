using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.CheckExistingLocationWithoutLocalLocation;

public class CheckExistingLocationWithoutLocalLocationCommandHandler : ICommandHandler<CheckExistingLocationWithoutLocalLocationCommand, Result<Guid>>
{
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IGenericRepository<LocalLocation> _localLocationRepository;
    private readonly IGenericRepository<Location> _locationRepository;
    private readonly IGenericRepository<CityLocalLocations> _cityLocalLocationRepository;

    public CheckExistingLocationWithoutLocalLocationCommandHandler(IGenericRepository<Country> countryRepository, 
        IGenericRepository<City> cityRepository, IGenericRepository<LocalLocation> localLocationRepository,
        IGenericRepository<Location> locationRepository, IGenericRepository<CityLocalLocations> cityLocalLocationRepository)
    {
        _countryRepository = countryRepository;
        _cityRepository = cityRepository;
        _localLocationRepository = localLocationRepository;
        _locationRepository = locationRepository;
        _cityLocalLocationRepository = cityLocalLocationRepository;
    }

    public async Task<Result<Guid>> Handle(CheckExistingLocationWithoutLocalLocationCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.CheckExistingLocationRequest.CountryId);

        if (country == null)
            return Result.Failure<Guid>(DomainErrors.Country.NotFound(request.CheckExistingLocationRequest.CountryId));

        var city = await _cityRepository.GetByIdAsync(request.CheckExistingLocationRequest.CityId);

        if (city == null)
            return Result.Failure<Guid>(DomainErrors.City.NotFound(request.CheckExistingLocationRequest.CityId));

        var localLocation = await _localLocationRepository.GetFirstOrDefaultAsync(x=>x.Name.ToLower() == ("NotSet").ToLower());

        if (localLocation == null)
        {
            localLocation = await _localLocationRepository.AddAsync
                (new LocalLocation() { Id = Guid.NewGuid(), Name = "NotSet" });

            await _localLocationRepository.SaveChangesAsync();
        }

        var cityLocalLocation = await _cityLocalLocationRepository.GetFirstOrDefaultAsync
            (x => x.CityId == city.Id && x.LocalLocationId == localLocation.Id);

        if (cityLocalLocation == null)
        {
            cityLocalLocation = await _cityLocalLocationRepository.AddAsync
                (new CityLocalLocations() { Id = Guid.NewGuid(), CityId = city.Id, LocalLocationId = localLocation.Id });
            await _cityLocalLocationRepository.SaveChangesAsync();
        }


        var location = await _locationRepository.GetFirstOrDefaultAsync
            (x => x.CityLocalLocationsId == cityLocalLocation.Id && x.CountryId == country.Id);

        if (location == null)
        {
            location = await _locationRepository.AddAsync
                (new Location() { Id = Guid.NewGuid(), CountryId = country.Id, CityLocalLocationsId = cityLocalLocation.Id });

            await _locationRepository.SaveChangesAsync();
        }

        return Result.Success(location.Id);
    }
}
