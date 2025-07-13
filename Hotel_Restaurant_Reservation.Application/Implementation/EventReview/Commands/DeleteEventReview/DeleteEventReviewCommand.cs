using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.DeleteEventReview;

public class DeleteEventReviewCommand : ICommand<Result>
{
    public DeleteEventReviewCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}