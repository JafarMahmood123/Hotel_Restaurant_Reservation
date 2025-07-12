namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RoomAmenity
{
    public Guid Id { get; set; }

    public Guid RoomId { get; set; }

    public Guid AmenityId { get; set; }

    public virtual Room Room { get; set; }

    public virtual Amenity Amenity { get; set; }

    public RoomAmenity()
    {
    }
}
