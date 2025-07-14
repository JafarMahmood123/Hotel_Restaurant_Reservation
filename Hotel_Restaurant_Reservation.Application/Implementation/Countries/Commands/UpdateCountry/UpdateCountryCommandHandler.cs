// Hotel_Restaurant_Reservation.Application/Implementation/Countries/Commands/UpdateCountry/UpdateCountryCommandHandler.cs
using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.UpdateCountry;

public class UpdateCountryCommandHandler : ICommandHandler<UpdateCountryCommand, Result<CountryResponse>>
{
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IMapper _mapper;

    public UpdateCountryCommandHandler(IGenericRepository<Country> countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<Result<CountryResponse>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id);

        if (country is null)
        {
            return Result.Failure<CountryResponse>(DomainErrors.Country.NotFound(request.Id));
        }

        if (country.Name == request.UpdateCountryRequest.Name)
        {
            return Result.Failure<CountryResponse>(DomainErrors.Country.SameName);
        }

        var existingCountryWithName = await _countryRepository.GetFirstOrDefaultAsync(c => c.Name == request.UpdateCountryRequest.Name && c.Id != request.Id);
        if (existingCountryWithName != null)
        {
            return Result.Failure<CountryResponse>(DomainErrors.Country.ExistingCountry(request.UpdateCountryRequest.Name));
        }

        _mapper.Map(request.UpdateCountryRequest, country);

        await _countryRepository.UpdateAsync(request.Id, country);
        await _countryRepository.SaveChangesAsync();

        var countryResponse = _mapper.Map<CountryResponse>(country);
        return Result.Success(countryResponse);
    }
}