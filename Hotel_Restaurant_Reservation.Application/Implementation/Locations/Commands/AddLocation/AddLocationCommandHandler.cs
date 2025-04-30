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

        Location location = request.Location;


        var existingLocation = await _genericRepository.GetFirstOrDefaultAsync(x => x.CityLocalLocationsId == location.CityLocalLocationsId
        && x.CountryId == location.CountryId);

        if (existingLocation != null)
        {
            location = existingLocation;
        }
        else
        {
            location.Id = Guid.NewGuid();
            location.CountryId = location.CountryId;
            location.CityLocalLocationsId = location.CityLocalLocationsId;

            location = await _genericRepository.AddAsync(location);
            await _genericRepository.SaveChangesAsync();
        }

        return location;
    }
}
