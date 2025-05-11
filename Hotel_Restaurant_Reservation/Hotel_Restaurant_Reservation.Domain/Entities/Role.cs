namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Role
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    // Navigation Properties

    public virtual ICollection<CustomerRoles> CustomerRoles { get; set; }

    public Role()
    {
        CustomerRoles = new HashSet<CustomerRoles>();
    }
}
