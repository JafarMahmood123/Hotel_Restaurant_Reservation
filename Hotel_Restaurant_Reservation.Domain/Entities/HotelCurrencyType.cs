namespace Hotel_Restaurant_Reservation.Domain.Entities
{
    public class HotelCurrencyType
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public Guid CurrencyTypeId { get; set; }
        public CurrencyType CurrencyType { get; set; }
    }
}