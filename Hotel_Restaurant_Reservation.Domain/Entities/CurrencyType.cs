namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class CurrencyType
{
    // Key Properties
    public Guid Id { get; set; }

    public string CurrencyCode { get; set; }

    // Foreign Keys

    public int HotelId { get; set; }

    public int EventId { get; set; }

    // Key Properties

    public virtual ICollection<Hotel> Hotels { get; set; }

    public virtual ICollection<Event> Events { get; set; }

    public CurrencyType()
    {
        Hotels = new HashSet<Hotel>();

        Events = new HashSet<Event>();
    }
}
