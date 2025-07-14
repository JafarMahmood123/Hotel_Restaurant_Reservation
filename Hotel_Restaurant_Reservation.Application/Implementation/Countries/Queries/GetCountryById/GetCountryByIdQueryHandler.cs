using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetCountryById;

public class GetCountryByIdQueryHandler : IQueryHandler<GetCountryByIdQuery, Result<CountryResponse>>
{
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IMapper _mapper;

    public GetCountryByIdQueryHandler(IGenericRepository<Country> countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<Result<CountryResponse>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id);

        if (country is null)
        {
            return Result.Failure<CountryResponse>(DomainErrors.Country.NotFound(request.Id));
        }

        var countryResponse = _mapper.Map<CountryResponse>(country);
        return Result.Success(countryResponse);
    }
}