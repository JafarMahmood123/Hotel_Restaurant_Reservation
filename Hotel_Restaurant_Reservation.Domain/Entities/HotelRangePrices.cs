namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class HotelRangePrices
{
    // Key Properties
    public Guid Id { get; set; }

    public double MinPrice { get; set; }

    public double MaxPrice { get; set; }

    // Foreign Keys

    public Guid HotelId { get; set; }

    // Navigation Properties

    public virtual ICollection<Hotel> Hotels { get; set; }

    public HotelRangePrices()
    {
        Hotels = new HashSet<Hotel>();
    }
}
