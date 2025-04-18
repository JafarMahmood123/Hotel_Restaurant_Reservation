namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Role
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public Guid CustomerId { get; set; }

    // Navigation Properties

    public virtual ICollection<Customer> Customers { get; set; }

    public Role()
    {
        Customers = new HashSet<Customer>();
    }
}
