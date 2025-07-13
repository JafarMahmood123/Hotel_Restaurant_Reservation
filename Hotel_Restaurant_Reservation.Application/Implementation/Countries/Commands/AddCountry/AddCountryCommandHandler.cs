using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using System.Collections.Specialized;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.AddCountry;

public class AddCountryCommandHandler : ICommandHandler<AddCountryCommand, Country>
{
    private readonly IGenericRepository<Country> _genericRepository;

    public AddCountryCommandHandler(IGenericRepository<Country> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Country> Handle(AddCountryCommand request, CancellationToken cancellationToken)
    {
        Country country = request.Country;

        var existingCountry = await _genericRepository.GetFirstOrDefaultAsync(x=>x.Name == country.Name);

        if (existingCountry != null)
        {
            country = existingCountry;
        }
        else
        {
            country.Id = Guid.NewGuid();
            country = await _genericRepository.AddAsync(country);
            await _genericRepository.SaveChangesAsync();
        }

        return country;
    }
}
