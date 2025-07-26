namespace Hotel_Restaurant_Reservation.Application.Implementation.Payments.Commands.PayRestaurantBooking
{
    public class PayRestaurantBookingRequest
    {
        public Guid RestaurantBookingId { get; set; }
        public Guid UserId { get; set; }
        public Guid CurrencyTypeId { get; set; }
        public decimal Amount { get; set; }
    }
}