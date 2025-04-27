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
        var localLocation = await genericRepository.GetByIdAsync(request.Id);

        return localLocation;
    }
}
