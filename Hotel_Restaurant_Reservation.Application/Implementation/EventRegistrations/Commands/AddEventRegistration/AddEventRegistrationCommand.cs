using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.AddEventRegistration;

public class AddEventRegistrationCommand : ICommand<Result<EventRegistrationResponse>>
{
    public AddEventRegistrationCommand(AddEventRegistrationRequest addEventRegistrationRequest)
    {
        AddEventRegistrationRequest = addEventRegistrationRequest;
    }

    public AddEventRegistrationRequest AddEventRegistrationRequest { get; }
}