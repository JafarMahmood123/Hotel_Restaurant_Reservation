namespace Hotel_Restaurant_Reservation.Domain.Entities
{
    public class HotelReview
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ReviewDateTime { get; set; }
        public string Description { get; set; }
        public double OverallRating { get; set; }
        public double ServiceRating { get; set; }
        public double CleanlinessRating { get; set; }
        public double ValueRating { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual User User { get; set; }
    }
}