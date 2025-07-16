namespace Hotel_Restaurant_Reservation.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
    }
}