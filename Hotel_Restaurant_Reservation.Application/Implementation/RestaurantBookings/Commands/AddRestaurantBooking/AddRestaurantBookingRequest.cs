using Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.AddBookingDishes;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking
{
    public class AddRestaurantBookingRequest
    {
        // Key Properties
        public DateTime ReceiveDateTime { get; set; }
        public TimeOnly BookingDurationTime { get; set; }
        public int NumberOfPeople { get; set; }
        public int TableNumber { get; set; }

        // Foreign Keys
        public Guid RestaurantId { get; set; }
        public Guid UserId { get; set; }
        public Guid CurrencyTypeId { get; set; } // Added CurrencyTypeId
        public AddBookingDishesRequest AddBookingDishRequest { get; set; }
    }
}
