namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Location
{
    // Key Properties
    public Guid Id { get; set; }

    // Foreign Keys

    public int CountryId { get; set; }

    public int CityId { get; set; }

    public int LocalLocationId { get; set; }

    public Guid RestaurantId  { get; set; }

    public Guid HotelId  { get; set; }

    public Guid EventId  { get; set; }

    public Guid CustomerId  { get; set; }

    // Navigation Properties

    public virtual Country? Country { get; set; }

    public virtual City? City { get; set; }

    public virtual LocalLocation? LocalLocation { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Hotel Hotel { get; set; }

    public virtual Restaurant Restaurant { get; set; }

    public virtual Event Event { get; set; }

    public Location()
    {
        
    }
}
