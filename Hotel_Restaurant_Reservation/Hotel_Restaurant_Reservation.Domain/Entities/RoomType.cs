namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RoomType
{
    // Key Properties
    public Guid Id { get; set; }

    public string Description { get; set; }

    // Foreign Keys

    public Guid RoomId { get; set; }

    // Navigation Properties

    public virtual ICollection<Room> Rooms { get; set; }

    public RoomType()
    {
        Rooms = new HashSet<Room>();
    }
}
