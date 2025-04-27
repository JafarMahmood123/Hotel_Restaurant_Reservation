namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class City
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public Guid CountryId { get; set; }

    // Navigation Properties

    public virtual ICollection<LocalLocation> LocalLocations { get; set; }

    public City()
    {
        LocalLocations = new HashSet<LocalLocation>();
    }
}
