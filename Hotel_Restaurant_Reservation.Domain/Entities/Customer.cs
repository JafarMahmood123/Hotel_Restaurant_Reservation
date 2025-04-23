namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Customer
{
    // Key Properties
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateOnly BirthDate { get; set; }

    public int Age { get; set; }

    public string Preferences { get; set; }

    // Foreign Keys

    public Guid RestaurantOrderId { get; set; }

    public Guid HotelReservationId { get; set; }

    public Guid RoleId { get; set; }

    public Guid ReviewId { get; set; }

    public Guid EventRegistrationId { get; set; }

    public Guid LocationId { get; set; }

    // Navigation Properties

    public virtual ICollection<RestaurantOrder> RestaurantOrders { get; set; }

    public virtual ICollection<HotelReservation> HotelReservations { get; set; }

    public virtual ICollection<Role> Roles { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }

    public virtual ICollection<EventRegistration> EventRegistrations { get; set; }

    public virtual Location Location { get; set; }

    public Customer()
    {
        RestaurantOrders = new HashSet<RestaurantOrder>();
        
        HotelReservations = new HashSet<HotelReservation>();

        Roles = new HashSet<Role>();

        Reviews = new HashSet<Review>();

        EventRegistrations = new HashSet<EventRegistration>();
    }
}
