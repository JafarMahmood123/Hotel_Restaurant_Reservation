    namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantBooking
{
    // Key Properties
    public Guid Id { get; set; }

    public DateTime BookingDateTime{ get; set; }

    public DateTime ReceiveDateTime { get; set; }

    public TimeOnly BookingDurationTime { get; set; }

    public int NumberOfPeople { get; set; }

    public int TableNumber { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid UserId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<BookingDish> BookingDishes { get; set; }

    public virtual RestaurantBookingPayment RestaurantBookingPayment { get; set; }

    public RestaurantBooking()
    {
        BookingDishes = new HashSet<BookingDish>();
    }
}
