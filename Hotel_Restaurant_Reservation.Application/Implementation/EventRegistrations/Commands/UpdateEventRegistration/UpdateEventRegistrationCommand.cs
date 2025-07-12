using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.UpdateEventRegistration;

public class UpdateEventRegistrationCommand : ICommand<Result<EventRegistrationResponse>>
{
    public UpdateEventRegistrationCommand(Guid id, UpdateEventRegistrationRequest updateEventRegistrationRequest)
    {
        Id = id;
        UpdateEventRegistrationRequest = updateEventRegistrationRequest;
    }

    public Guid Id { get; }
    public UpdateEventRegistrationRequest UpdateEventRegistrationRequest { get; }
}