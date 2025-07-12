using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.DeleteEvent;

public class DeleteEventCommand : ICommand<Result>
{
    public DeleteEventCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}