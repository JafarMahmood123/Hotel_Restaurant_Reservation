using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.UpdateLocation;

public class UpdateLocationCommand : ICommand<Location?>
{
    public UpdateLocationCommand(Guid id, Location location)
    {
        Id = id;
        Location = location;
    }

    public Guid Id { get; }
    public Location Location { get; }
}
