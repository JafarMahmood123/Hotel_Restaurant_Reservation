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
        var city = await genericRepository.GetByIdAsync(request.Id);

        if(city is not null)
        {
            if(city.Name == request.City.Name)
                return city;

            city = await genericRepository.UpdateAsync(request.Id, request.City);

            await genericRepository.SaveChangesAsync();
        }

        return city;
    }
}
