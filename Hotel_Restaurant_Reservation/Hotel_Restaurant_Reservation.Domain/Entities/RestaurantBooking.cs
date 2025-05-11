    namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantBooking
{
    // Key Properties
    public Guid Id { get; set; }

    public DateTime BookingDateTime{ get; set; }

    public DateTime ReceiveDateTime { get; set; }

    public DateTime EndBookingDateTime { get; set; }

    public int NumberOfPeople { get; set; }

    public int TableNumber { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid CustomerId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual Customer Customers { get; set; }

    public virtual ICollection<BookingDish> BookingDishes { get; set; }

    public RestaurantBooking()
    {
        BookingDishes = new HashSet<BookingDish>();
    }
}
