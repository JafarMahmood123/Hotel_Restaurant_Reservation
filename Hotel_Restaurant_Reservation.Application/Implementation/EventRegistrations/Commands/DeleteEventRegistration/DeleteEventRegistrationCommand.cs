using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.DeleteEventRegistration;

public class DeleteEventRegistrationCommand : ICommand<Result>
{
    public DeleteEventRegistrationCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}