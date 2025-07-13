using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries.GetAllEventReviews;

public class GetAllEventReviewsQuery : IQuery<Result<IEnumerable<EventReviewResponse>>>
{
}