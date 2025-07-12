namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class HotelAmenityPrice
{
    public Guid Id { get; set; }

    public Guid HotelId { get; set; }

    public Guid AmenityId { get; set; }

    public double Price { get; set; }

    public virtual Hotel Hotel { get; set; }

    public virtual Amenity Amenity { get; set; }
}
