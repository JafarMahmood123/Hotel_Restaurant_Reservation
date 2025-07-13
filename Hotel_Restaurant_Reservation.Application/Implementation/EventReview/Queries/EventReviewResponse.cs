namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries
{
    public class EventReviewResponse
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ReviewDateTime { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
    }
}