using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.UpdateCity;

public class UpdateCityCommandHandler : ICommandHandler<UpdateCityCommand, Result<CityResponse>>
{
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IMapper _mapper;

    public UpdateCityCommandHandler(IGenericRepository<City> cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<Result<CityResponse>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.Id);

        if (city is null)
        {
            return Result.Failure<CityResponse>(DomainErrors.City.NotFound(request.Id));
        }

        if (city.Name == request.UpdateCityRequest.Name)
        {
            return Result.Failure<CityResponse>(DomainErrors.City.SameName);
        }

        var existingCity = await _cityRepository.GetFirstOrDefaultAsync(c => c.Name == request.UpdateCityRequest.Name && c.CountryId == city.CountryId);
        if (existingCity != null)
        {
            return Result.Failure<CityResponse>(DomainErrors.City.ExistingCity(request.UpdateCityRequest.Name, city.CountryId));
        }

        _mapper.Map(request.UpdateCityRequest, city);

        await _cityRepository.UpdateAsync(request.Id, city);
        await _cityRepository.SaveChangesAsync();

        var cityResponse = _mapper.Map<CityResponse>(city);
        return Result.Success(cityResponse);
    }
}