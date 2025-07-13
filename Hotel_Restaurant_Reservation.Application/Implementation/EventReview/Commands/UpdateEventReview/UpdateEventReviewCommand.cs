using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.UpdateEventReview;

public class UpdateEventReviewCommand : ICommand<Result<EventReviewResponse>>
{
    public UpdateEventReviewCommand(Guid id, UpdateEventReviewRequest updateEventReviewRequest)
    {
        Id = id;
        UpdateEventReviewRequest = updateEventReviewRequest;
    }

    public Guid Id { get; }
    public UpdateEventReviewRequest UpdateEventReviewRequest { get; }
}