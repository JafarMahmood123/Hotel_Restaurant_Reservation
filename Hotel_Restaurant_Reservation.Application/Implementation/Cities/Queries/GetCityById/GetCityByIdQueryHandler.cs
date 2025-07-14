using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityById;

public class GetCityByIdQueryHandler : IQueryHandler<GetCityByIdQuery, Result<CityResponse>>
{
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IMapper _mapper;

    public GetCityByIdQueryHandler(IGenericRepository<City> cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<Result<CityResponse>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.Id);

        if (city is null)
        {
            return Result.Failure<CityResponse>(DomainErrors.City.NotFound(request.Id));
        }

        var cityResponse = _mapper.Map<CityResponse>(city);
        return Result.Success(cityResponse);
    }
}