using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.UpdateLocalLocation;

public class UpdateLocalLocationCommand : ICommand<LocalLocation?>
{
    public UpdateLocalLocationCommand(Guid id, LocalLocation localLocation)
    {
        Id = id;
        LocalLocation = localLocation;
    }

    public Guid Id { get; }
    public LocalLocation LocalLocation { get; }
}
