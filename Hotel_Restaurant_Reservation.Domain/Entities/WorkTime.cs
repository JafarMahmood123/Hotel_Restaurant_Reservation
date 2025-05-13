namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class WorkTime
{
    // Key Properties
    public Guid Id { get; set; }

    public DayOfWeek Day { get; set; }

    public TimeOnly OpenHour { get; set; }

    public TimeOnly CloseHour { get; set; }

    // Foreign Keys


    // Navigation Properties

    public virtual ICollection<RestaurantWorkTime> RestaurantWorkTimes { get; set; }

    public WorkTime()
    {
        RestaurantWorkTimes = new HashSet<RestaurantWorkTime>();
    }
}
