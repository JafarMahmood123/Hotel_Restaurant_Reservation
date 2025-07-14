using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.AddCity;

public class AddCityCommandHandler : ICommandHandler<AddCityCommand, Result<CityResponse>>
{
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IMapper _mapper;

    public AddCityCommandHandler(
        IGenericRepository<City> cityRepository,
        IGenericRepository<Country> countryRepository,
        IMapper mapper)
    {
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<Result<CityResponse>> Handle(AddCityCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.AddCityRequest.CountryId);
        if (country is null)
        {
            return Result.Failure<CityResponse>(DomainErrors.Country.NotFound(request.AddCityRequest.CountryId));
        }

        var existingCity = await _cityRepository.GetFirstOrDefaultAsync(
            x => x.Name == request.AddCityRequest.Name && x.CountryId == request.AddCityRequest.CountryId);

        if (existingCity != null)
        {
            return Result.Failure<CityResponse>(DomainErrors.City.ExistingCity(request.AddCityRequest.Name, request.AddCityRequest.CountryId));
        }

        var city = _mapper.Map<City>(request.AddCityRequest);
        city.Id = Guid.NewGuid();

        await _cityRepository.AddAsync(city);
        await _cityRepository.SaveChangesAsync();

        var cityResponse = _mapper.Map<CityResponse>(city);

        return Result.Success(cityResponse);
    }
}