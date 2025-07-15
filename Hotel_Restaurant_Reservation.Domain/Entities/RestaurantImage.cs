namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantImage
{
    public Guid Id { get; set; }

    public string Url { get; set; }

    public Guid RestaurantId { get; set; }

    public virtual Restaurant Restaurant { get; set; }
}
