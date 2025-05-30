using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocation;

public class AddLocationCommand : ICommand<Location>
{

    public AddLocationCommand(Location location)
    {
        Location = location;
    }

    public Location Location { get; }
}
