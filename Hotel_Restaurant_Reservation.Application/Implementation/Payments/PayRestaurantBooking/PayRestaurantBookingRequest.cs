namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.PayRestaurantBooking
{
    public class PayRestaurantBookingRequest
    {
        public Guid RestaurantBookingId { get; set; }
        public string CurrencyCode { get; set; }
    }
}
