namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Location
{
    // Key Properties
    public Guid Id { get; set; }

    // Foreign Keys

    public Guid CountryId { get; set; }

    public Guid CityLocalLocationsId { get; set; } 

    // Navigation Properties

    public virtual Country Country { get; set; }

    public virtual CityLocalLocations CityLocalLocations { get; set; }

    public virtual ICollection<Customer?> Customer { get; set; }

    public virtual ICollection<Hotel?> Hotel { get; set; }

    public virtual ICollection<Restaurant?> Restaurant { get; set; }

    public virtual ICollection<Event?> Event { get; set; }

    public Location()
    {
        
    }
}
