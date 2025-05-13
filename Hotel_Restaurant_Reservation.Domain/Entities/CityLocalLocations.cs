namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class CityLocalLocations
{
    // Key Properties

    public Guid Id { get; set; }

    // Foreign Keys

    public Guid CityId { get; set; }

    public Guid LocalLocationId { get; set; }

    // Navigation Properties

    public virtual City City { get; set; }

    public virtual LocalLocation LocalLocation { get; set; }

    public CityLocalLocations()
    {

    }
}
