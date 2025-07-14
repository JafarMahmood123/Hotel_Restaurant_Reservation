using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCitiesByCountryId;

public class GetCitiesByCountryIdQueryHandler : IQueryHandler<GetCitiesByCountryIdQuery, Result<IEnumerable<CityResponse>>>
{
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IMapper _mapper;

    public GetCitiesByCountryIdQueryHandler(IGenericRepository<City> cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CityResponse>>> Handle(GetCitiesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cityRepository
            .Where(c => c.CountryId == request.CountryId)
            .ToListAsync(cancellationToken);

        var cityResponses = _mapper.Map<IEnumerable<CityResponse>>(cities);

        return Result.Success(cityResponses);
    }
}