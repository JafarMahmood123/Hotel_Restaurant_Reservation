namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RoomImage
{
    public Guid Id { get; set; }

    public string ImageUrl { get; set; }    

    public Guid RoomId { get; set; }

    public Room Room { get; set; }
}
