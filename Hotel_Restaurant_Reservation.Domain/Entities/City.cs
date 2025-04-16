namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class City
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public Guid LocationId { get; set; }

    // Navigation Properties

    public virtual ICollection<Location> Locations { get; set; }

    public City()
    {
        Locations = new HashSet<Location>();
    }
}
