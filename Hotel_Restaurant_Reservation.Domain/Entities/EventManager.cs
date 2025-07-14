namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class EventManager
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<Event> Events { get; set; }
}
