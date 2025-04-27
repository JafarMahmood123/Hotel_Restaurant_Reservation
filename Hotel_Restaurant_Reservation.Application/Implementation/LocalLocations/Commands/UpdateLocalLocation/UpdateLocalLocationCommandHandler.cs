using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.UpdateLocalLocation;

public class UpdateLocalLocationCommandHandler : ICommandHandler<UpdateLocalLocationCommand, LocalLocation?>
{
    private readonly IGenericRepository<LocalLocation> genericRepository;

    public UpdateLocalLocationCommandHandler(IGenericRepository<LocalLocation> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<LocalLocation?> Handle(UpdateLocalLocationCommand request, CancellationToken cancellationToken)
    {
        var localLocation = await genericRepository.GetByIdAsync(request.Id);

        if (localLocation is not null)
        {
            if (localLocation.Name == request.LocalLocation.Name)
                return localLocation;

            localLocation = await genericRepository.UpdateAsync(request.Id, request.LocalLocation);

            await genericRepository.SaveChangesAsync();
        }

        return localLocation;
    }
}
