using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.AddEventReview
{
    public class AddEventReviewCommand : ICommand<Result<EventReviewResponse>>
    {
        public AddEventReviewRequest AddEventReviewRequest { get; }

        public AddEventReviewCommand(AddEventReviewRequest addEventReviewRequest)
        {
            AddEventReviewRequest = addEventReviewRequest;
        }
    }
}