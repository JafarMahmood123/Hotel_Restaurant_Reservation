using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.DeleteLcoation;

public class DeleteLcoationCommandHandler : ICommandHandler<DeleteLcoationCommand, Location?>
{
    private readonly IGenericRepository<Location> genericRepository;

    public DeleteLcoationCommandHandler(IGenericRepository<Location> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<Location?> Handle(DeleteLcoationCommand request, CancellationToken cancellationToken)
    {
        var location = await genericRepository.RemoveAsync(request.Id);

        if (location == null) 
            return null;

        await genericRepository.SaveChangesAsync();

        return location;
    }
}
