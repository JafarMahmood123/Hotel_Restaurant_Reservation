namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.AddEventReview
{
    public class AddEventReviewRequest
    {
        public Guid EventId { get; set; }
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
    }
}