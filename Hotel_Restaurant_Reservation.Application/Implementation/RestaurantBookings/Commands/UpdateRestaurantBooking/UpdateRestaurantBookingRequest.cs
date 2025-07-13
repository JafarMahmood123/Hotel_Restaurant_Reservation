namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.UpdateRestaurantBooking;

public class UpdateRestaurantBookingRequest
{
    public DateTime ReceiveDateTime { get; set; }
    public TimeOnly BookingDurationTime { get; set; }
    public int NumberOfPeople { get; set; }
    public int TableNumber { get; set; }
}