using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetAllCountries;

public class GetAllCountriesQueryHandler : IQueryHandler<GetAllCountriesQuery, Result<IEnumerable<CountryResponse>>>
{
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IMapper _mapper;

    public GetAllCountriesQueryHandler(IGenericRepository<Country> countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CountryResponse>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await _countryRepository.GetAllAsync();
        var countryResponses = _mapper.Map<IEnumerable<CountryResponse>>(countries);
        return Result.Success(countryResponses);
    }
}