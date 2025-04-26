using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocationCommand;

public class AddLocationCommand : ICommand<Location>
{
    public Country Country { get; set; }

    public City City { get; set; }

    public LocalLocation LocalLocation { get; set; }

    public AddLocationCommand(Country country, City city, LocalLocation localLocation)
    {
        City = city;
        LocalLocation = localLocation;
        Country = country;
    }
}
