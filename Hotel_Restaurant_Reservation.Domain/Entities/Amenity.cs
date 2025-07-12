namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Amenity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<RoomAmenity> RoomAmenities { get; set; }

    public Amenity()
    {
        RoomAmenities = new HashSet<RoomAmenity>();
    }
}
