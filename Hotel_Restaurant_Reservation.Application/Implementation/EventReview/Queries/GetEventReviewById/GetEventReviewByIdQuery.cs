using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries.GetEventReviewById;

public class GetEventReviewByIdQuery : IQuery<Result<EventReviewResponse>>
{
    public GetEventReviewByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}