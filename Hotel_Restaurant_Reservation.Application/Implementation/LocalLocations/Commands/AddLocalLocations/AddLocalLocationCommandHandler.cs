using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocation;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocations;

public class AddLocalLocationCommandHandler : ICommandHandler<AddLocalLocationCommand, LocalLocation>
{
    private readonly IGenericRepository<LocalLocation> localLocationRepository;
    private readonly IGenericRepository<CityLocalLocations> cityLocalLocationRepository;

    public AddLocalLocationCommandHandler(IGenericRepository<LocalLocation> localLocationRepository,
        IGenericRepository<CityLocalLocations> cityLocalLocationRepository)
    {
        this.localLocationRepository = localLocationRepository;
        this.cityLocalLocationRepository = cityLocalLocationRepository;
    }

    public async Task<LocalLocation> Handle(AddLocalLocationCommand request, CancellationToken cancellationToken)
    {
        LocalLocation localLocation = request.LocalLocation;

        var existingLocation = await localLocationRepository.GetFirstOrDefaultAsync(x=>x.Name == localLocation.Name);

        if (existingLocation != null)
        {
            localLocation = existingLocation;
        }
        else
        {
            localLocation.Id = Guid.NewGuid();
            localLocation = await localLocationRepository.AddAsync(localLocation);
            await localLocationRepository.SaveChangesAsync();
        }

        var existingCityLocalLocation = await cityLocalLocationRepository.GetFirstOrDefaultAsync(x => x.LocalLocationId == localLocation.Id
        && x.CityId == request.CityId);

        if (existingCityLocalLocation is null)
        {
            var cityLocalLocation = new CityLocalLocations();
            cityLocalLocation.Id = Guid.NewGuid();
            cityLocalLocation.CityId = request.CityId;
            cityLocalLocation.LocalLocationId = localLocation.Id;

            await cityLocalLocationRepository.AddAsync(cityLocalLocation);
            await cityLocalLocationRepository.SaveChangesAsync();
        }

        return localLocation;
    }
}
