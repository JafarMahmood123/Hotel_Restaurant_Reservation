namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class CustomerRoles
{
    // Key Properties
    public Guid Id { get; set; }

    // Foreign Keys

    public Guid CustomerId { get; set; }

    public Guid RoleId { get; set; }

    // Navigation Properties

    public virtual Customer Customer { get; set; }

    public virtual Role Role { get; set; }

    public CustomerRoles()
    {
        
    }
}
