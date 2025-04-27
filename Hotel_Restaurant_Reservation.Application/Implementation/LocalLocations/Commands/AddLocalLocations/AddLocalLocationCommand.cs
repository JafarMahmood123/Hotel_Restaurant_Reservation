using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocation;

public class AddLocalLocationCommand : ICommand<LocalLocation>
{
    public LocalLocation LocalLocation;

    public AddLocalLocationCommand(LocalLocation localLocation)
    {
        LocalLocation = localLocation;
    }
}
