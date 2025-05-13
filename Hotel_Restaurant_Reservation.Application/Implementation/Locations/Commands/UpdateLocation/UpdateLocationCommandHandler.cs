using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.UpdateLocation;

public class UpdateLocationCommandHandler : ICommandHandler<UpdateLocationCommand, Location?>
{
    private readonly IGenericRepository<Location> genericRepository;

    public UpdateLocationCommandHandler(IGenericRepository<Location> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<Location?> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var existingLocation = await genericRepository.GetByIdAsync(request.Id);

        //if (existingLocation is not null)
        //{
        //    if (existingLocation.Name == request.localLocation.Name)
        //        return city;

        //    request.City.CountryId = city.CountryId;
        //    city = await genericRepository.UpdateAsync(request.Id, request.City);

        //    await genericRepository.SaveChangesAsync();
        //}

        //return city;

        return existingLocation;
    }
}
