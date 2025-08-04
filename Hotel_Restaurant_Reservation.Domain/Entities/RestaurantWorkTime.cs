namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantWorkTime
{
    // Key Properties

    public Guid Id { get; set; }

    public string Day { get; set; }

    public TimeOnly OpenHour { get; set; }

    public TimeOnly CloseHour { get; set; }

    public Guid RestaurantId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public RestaurantWorkTime()
    {

    }
}
