namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class LocalLocation
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public Guid CityId { get; set; }

    // Navigation Properties

    public virtual ICollection<Location> Locations { get; set; }

    public LocalLocation()
    {
        Locations = new HashSet<Location>();
    }

}
