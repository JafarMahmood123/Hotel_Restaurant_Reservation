namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantOrder
{
    // Key Properties
    public Guid Id { get; set; }

    public DateTime BookingDateTime{ get; set; }

    public DateTime ReceiveDate { get; set; }

    public int NumberOfPeople { get; set; }

    public int TableNumber { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid CustomerId { get; set; }

    // Navigation Properties

    public virtual ICollection<Restaurant> Restaurants { get; set; }

    public virtual ICollection<Customer> Customers { get; set; }

    public RestaurantOrder()
    {
        Restaurants = new HashSet<Restaurant>();

        Customers = new HashSet<Customer>();
    }
}
