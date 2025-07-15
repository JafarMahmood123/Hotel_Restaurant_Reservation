namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class EventImage
{
    public Guid Id { get; set; }

    public string Url { get; set; }

    public Guid EventId { get; set; }

    public virtual Event Event { get; set; }
}
