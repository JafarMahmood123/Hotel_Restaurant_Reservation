namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RoomAmenity
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public int RoomId { get; set; }

    // Navigation Properties

    public virtual ICollection<Room> Rooms { get; set; }

    public RoomAmenity()
    {
        Rooms = new HashSet<Room>();
    }
}
