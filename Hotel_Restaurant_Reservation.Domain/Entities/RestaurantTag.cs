namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantTag
{
    // Key Properties

    public Guid Id { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid TagId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual Tag Tag { get; set; }

    public RestaurantTag()
    {

    }
}
