using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocation;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocations;

public class AddLocalLocationCommandHandler : ICommandHandler<AddLocalLocationCommand, LocalLocation>
{
    private readonly IGenericRepository<LocalLocation> _genericRepository;

    public AddLocalLocationCommandHandler(IGenericRepository<LocalLocation> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<LocalLocation> Handle(AddLocalLocationCommand request, CancellationToken cancellationToken)
    {
        LocalLocation localLocation = request.LocalLocation;

        var existingLocation = await _genericRepository.GetFirstOrDefaultAsync(x=>x.Name == localLocation.Name);

        if (existingLocation != null)
        {
            localLocation = existingLocation;
        }
        else
        {
            localLocation.Id = Guid.NewGuid();
            localLocation = await _genericRepository.AddAsync(localLocation);
            await _genericRepository.SaveChangesAsync();
        }

        return localLocation;
    }
}
