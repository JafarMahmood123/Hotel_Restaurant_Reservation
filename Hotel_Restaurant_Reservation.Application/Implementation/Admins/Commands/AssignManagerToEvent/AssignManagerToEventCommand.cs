using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.AssignManagerToEvent
{
    public class AssignManagerToEventCommand : ICommand<Result>
    {
        public Guid EventId { get; set; }
        public Guid ManagerId { get; set; }
    }
}