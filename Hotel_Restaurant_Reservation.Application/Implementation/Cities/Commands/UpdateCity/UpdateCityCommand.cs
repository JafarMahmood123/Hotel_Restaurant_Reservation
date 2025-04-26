using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.UpdateCity;

public class UpdateCityCommand : ICommand<City?>
{
    public City City { get; set; }

    public Guid Id { get; set; }

    public UpdateCityCommand(Guid id, City city)
    {
        Id = id;
        City = city;
    }
}
