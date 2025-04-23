namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class PropertyType
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public Guid HotelId { get; set; }

    // Navigation Properties

    public ICollection<Hotel> Hotels { get; set; }

    public PropertyType()
    {
        Hotels = new HashSet<Hotel>();
    }
}
