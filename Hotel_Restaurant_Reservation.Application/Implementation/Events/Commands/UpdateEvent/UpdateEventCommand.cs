using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.UpdateEvent;

public class UpdateEventCommand : ICommand<Result<EventResponse>>
{
    public UpdateEventCommand(Guid id, UpdateEventRequest updateEventRequest)
    {
        Id = id;
        UpdateEventRequest = updateEventRequest;
    }

    public Guid Id { get; }
    public UpdateEventRequest UpdateEventRequest { get; }
}