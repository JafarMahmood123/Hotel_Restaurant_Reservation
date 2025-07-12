using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries.GetAllEventRegistrationsByEventId;

public class GetAllEventRegistrationsByEventIdQuery : IQuery<Result<IEnumerable<EventRegistrationResponse>>>
{
    public GetAllEventRegistrationsByEventIdQuery(Guid eventId)
    {
        EventId = eventId;
    }

    public Guid EventId { get; }
}