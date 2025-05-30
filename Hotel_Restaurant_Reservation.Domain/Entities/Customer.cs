using Hotel_Restaurant_Reservation.Domain.Enums;

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

    public Roles Role { get; set; }

    // Foreign Keys

    public Guid LocationId { get; set; }

    // Navigation Properties

    public virtual Location Location { get; set; }

    public Customer()
    {
    }
}
