using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.AddCountry;

public class AddCountryCommandHandler : ICommandHandler<AddCountryCommand, Result<CountryResponse>>
{
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IMapper _mapper;

    public AddCountryCommandHandler(IGenericRepository<Country> countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<Result<CountryResponse>> Handle(AddCountryCommand request, CancellationToken cancellationToken)
    {
        var country = _mapper.Map<Country>(request.AddCountryRequest);

        var existingCountry = await _countryRepository.GetFirstOrDefaultAsync(x => x.Name == country.Name);

        if (existingCountry != null)
        {
            return Result.Failure<CountryResponse>(DomainErrors.Country.ExistingCountry(country.Name));
        }

        country.Id = Guid.NewGuid();
        await _countryRepository.AddAsync(country);
        await _countryRepository.SaveChangesAsync();

        var countryResponse = _mapper.Map<CountryResponse>(country);

        return Result.Success(countryResponse);
    }
}