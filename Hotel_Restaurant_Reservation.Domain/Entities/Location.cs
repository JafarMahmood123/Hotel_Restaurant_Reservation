namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Location
{
    // Key Properties
    public Guid Id { get; set; }

    // Foreign Keys

    public Guid CountryId { get; set; }

    public Guid CityId { get; set; }

    public Guid LocalLocationId { get; set; }

    // Navigation Properties

    public virtual Country? Country { get; set; }

    public virtual City? City { get; set; }

    public virtual LocalLocation? LocalLocation { get; set; }

    public Location()
    {
        
    }
}
