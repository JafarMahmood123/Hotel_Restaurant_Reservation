namespace Hotel_Restaurant_Reservation.Domain.Entities
{
    public class EventCurrencyType
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid CurrencyTypeId { get; set; }
        public CurrencyType CurrencyType { get; set; }
    }
}
