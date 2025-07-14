namespace Hotel_Restaurant_Reservation.Domain.Entities
{
    public class EventReview
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ReviewDateTime { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }

        public virtual Event Event { get; set; }
        public virtual User Customer { get; set; }
    }
}