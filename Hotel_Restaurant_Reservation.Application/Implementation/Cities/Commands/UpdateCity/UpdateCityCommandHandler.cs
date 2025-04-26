using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.UpdateCity;

public class UpdateCityCommandHandler : ICommandHandler<UpdateCityCommand, City?>
{
    private readonly IGenericRepository<City> genericRepository;

    public UpdateCityCommandHandler(IGenericRepository<City> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<City?> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var city = await genericRepository.UpdateAsync(request.Id, request.City);

        if (city is not null)
            await genericRepository.SaveChangesAsync();

        return city;
    }
}
