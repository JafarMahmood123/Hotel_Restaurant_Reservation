using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.AddEvent;

public class AddEventCommand : ICommand<Result<EventResponse>>
{
    public AddEventCommand(AddEventRequest addEventRequest)
    {
        AddEventRequest = addEventRequest;
    }

    public AddEventRequest AddEventRequest { get; }
}