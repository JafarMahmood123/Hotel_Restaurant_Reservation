using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries.GetEventRegistrationById;

public class GetEventRegistrationByIdQuery : IQuery<Result<EventRegistrationResponse>>
{
    public GetEventRegistrationByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}