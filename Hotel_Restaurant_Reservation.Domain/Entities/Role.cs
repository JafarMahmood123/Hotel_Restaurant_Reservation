namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Role
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }


    // Navigation Properties

    public virtual ICollection<Customer> Customer { get; set; }

    public Role()
    {
    }
}
