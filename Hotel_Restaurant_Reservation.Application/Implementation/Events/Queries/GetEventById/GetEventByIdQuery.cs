using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries.GetEventById;

public class GetEventByIdQuery : IQuery<Result<EventResponse>>
{
    public GetEventByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}