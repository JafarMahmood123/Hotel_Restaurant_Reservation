using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityByName;

public class GetCityByNameQueryHandler : IQueryHandler<GetCityByNameQuery, Result<CityResponse>>
{
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IMapper _mapper;

    public GetCityByNameQueryHandler(IGenericRepository<City> cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<Result<CityResponse>> Handle(GetCityByNameQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetFirstOrDefaultAsync(c => c.Name == request.Name);

        if (city is null)
        {
            return Result.Failure<CityResponse>(DomainErrors.City.NotFoundByName(request.Name));
        }

        var cityResponse = _mapper.Map<CityResponse>(city);
        return Result.Success(cityResponse);
    }
}