using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocationCommand;

public class AddLocationCommandHandler : ICommandHandler<AddLocationCommand, Location>
{
    private readonly IGenericRepository<Location> _genericRepository;

    public AddLocationCommandHandler(IGenericRepository<Location> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Location> Handle(AddLocationCommand request, CancellationToken cancellationToken)
    {
        Country country = request.Country;
        City city = request.City;
        LocalLocation localLocation = request.LocalLocation;

        Location location = new Location(); 


        var existingLocation = await _genericRepository.GetFirstOrDefaultAsync(x => x.LocalLocationId == localLocation.Id
        && x.CityId == city.Id && x.CountryId == country.Id);

        if (existingLocation != null)
        {
            location = existingLocation;
        }
        else
        {
            location.Id = Guid.NewGuid();
            location.CountryId = country.Id;
            location.CityId = city.Id;
            location.LocalLocationId = localLocation.Id;

            location = await _genericRepository.AddAsync(location);
            await _genericRepository.SaveChangesAsync();
        }

        return location;
    }
}
