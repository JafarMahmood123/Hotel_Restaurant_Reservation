using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.AddCity;

public class AddCityCommandHandler : ICommandHandler<AddCityCommand, City>
{
    private readonly IGenericRepository<City> genericRepository;

    public AddCityCommandHandler(IGenericRepository<City> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<City> Handle(AddCityCommand request, CancellationToken cancellationToken)
    {
        City city = request.City;

        var existingCity = await genericRepository.GetFirstOrDefaultAsync(x=>x.Name == city.Name);

        if(existingCity is not null)
        {
            city = existingCity;
        }
        else
        {
            city.Id = Guid.NewGuid();
            city = await genericRepository.AddAsync(city);
             await genericRepository.SaveChangesAsync();
        }

        return city;
    }
}
