namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantWorkTime
{
    // Key Properties

    public Guid Id { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid WorkTimeId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual WorkTime WorkTime { get; set; }

    public RestaurantWorkTime()
    {

    }
}
