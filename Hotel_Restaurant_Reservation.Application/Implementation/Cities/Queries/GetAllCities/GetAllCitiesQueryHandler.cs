using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetAllCities;

public class GetAllCitiesQueryHandler : IQueryHandler<GetAllCitiesQuery, Result<IEnumerable<CityResponse>>>
{
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IMapper _mapper;

    public GetAllCitiesQueryHandler(IGenericRepository<City> cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CityResponse>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cityRepository.GetAllAsync();
        var cityResponses = _mapper.Map<IEnumerable<CityResponse>>(cities);
        return Result.Success(cityResponses);
    }
}