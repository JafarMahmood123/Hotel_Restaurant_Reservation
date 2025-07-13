namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries
{
    public class HotelReviewResponse
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ReviewDateTime { get; set; }
        public string Description { get; set; }
        public double OverallRating { get; set; }
        public double ServiceRating { get; set; }
        public double CleanlinessRating { get; set; }
        public double ValueRating { get; set; }
    }
}