using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.DeleteCity;

public class DeleteCityCommandHandler : ICommandHandler<DeleteCityCommand, City?>
{
    private readonly IGenericRepository<City> genericRepository;

    public DeleteCityCommandHandler(IGenericRepository<City> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<City?> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await genericRepository.RemoveAsync(request.Id);

        if (city == null)
            return null;

        await genericRepository.SaveChangesAsync();

        return city;
    }
}
