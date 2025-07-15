using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries.GetAllEventReviewsByEventId
{
    public class GetAllEventReviewsByEventIdQuery : IQuery<Result<IEnumerable<EventReviewResponse>>>
    {
        public GetAllEventReviewsByEventIdQuery(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; }
    }
}