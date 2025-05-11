using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.AddCity;

public class AddCityCommandHandler : ICommandHandler<AddCityCommand, City>
{
    private readonly IGenericRepository<City> cityRepository;

    public AddCityCommandHandler(IGenericRepository<City> cityRepository)
    {
        this.cityRepository = cityRepository;
    }

    public async Task<City> Handle(AddCityCommand request, CancellationToken cancellationToken)
    {
        City city = request.City;

        var existingCity = await cityRepository.GetFirstOrDefaultAsync(x => x.Name == city.Name && x.CountryId == request.CountryId);

        if(existingCity is not null)
        {
            city = existingCity;
        }
        else
        {
            city.Id = Guid.NewGuid();
            city.CountryId = request.CountryId;
            city = await cityRepository.AddAsync(city);
             await cityRepository.SaveChangesAsync();
        }

        return city;
    }
}
