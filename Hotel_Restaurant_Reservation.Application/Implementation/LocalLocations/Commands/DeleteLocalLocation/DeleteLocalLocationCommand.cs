using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.DeleteLocalLocation;

public class DeleteLocalLocationCommand : ICommand<LocalLocation?>
{
    public DeleteLocalLocationCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
