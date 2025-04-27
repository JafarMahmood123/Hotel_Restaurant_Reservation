using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.DeleteLocalLocation;

public class DeleteLocalLocationCommandHandler : ICommandHandler<DeleteLocalLocationCommand, LocalLocation?>
{
    private readonly IGenericRepository<LocalLocation> genericRepository;

    public DeleteLocalLocationCommandHandler(IGenericRepository<LocalLocation> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<LocalLocation?> Handle(DeleteLocalLocationCommand request, CancellationToken cancellationToken)
    {
        var localLocation = await genericRepository.GetByIdAsync(request.Id);

        if (localLocation is not null)
        {
            localLocation = genericRepository.Remove(localLocation);

            await genericRepository.SaveChangesAsync();
        }

        return localLocation;
    }
}
