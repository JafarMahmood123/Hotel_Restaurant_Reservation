using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocation;

public class AddLocalLocationCommand : ICommand<LocalLocation>
{

    public AddLocalLocationCommand(LocalLocation localLocation, Guid cityId)
    {
        LocalLocation = localLocation;
        CityId = cityId;
    }

    public LocalLocation LocalLocation;
    public Guid CityId { get; }
}
