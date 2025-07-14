namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class HotelManager
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; }
}
