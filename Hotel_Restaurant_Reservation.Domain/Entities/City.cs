namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class City
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public Guid CountryId { get; set; }

    // Navigation Properties

    public virtual ICollection<CityLocalLocations> CityLocalLocations { get; set; }

    public City()
    {
        CityLocalLocations = new HashSet<CityLocalLocations>();
    }
}
