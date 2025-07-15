using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetEventImages
{
    public class GetEventImagesByEventIdQuery : IQuery<Result<List<string>>>
    {
        public GetEventImagesByEventIdQuery(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; }
    }
}