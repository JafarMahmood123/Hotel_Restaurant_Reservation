using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.DeleteLcoation;

public class DeleteLcoationCommand : ICommand<Location?>
{
    public DeleteLcoationCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
