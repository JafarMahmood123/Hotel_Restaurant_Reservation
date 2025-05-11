namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class LocalLocation
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    // Navigation Properties

    public virtual ICollection<CityLocalLocations> CityLocalLocations { get; set; }

    public LocalLocation()
    {
        CityLocalLocations = new HashSet<CityLocalLocations>();
    }

}
