namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class WorkTime
{
    // Key Properties
    public Guid Id { get; set; }

    public DayOfWeek Day { get; set; }

    public TimeOnly OpenHour { get; set; }

    public TimeOnly CloseHour { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurants { get; set; }

    public WorkTime()
    {
        
    }

    public override string ToString()
    {
        return "Day = " + Day + ", Open Hour = " + OpenHour + ", Close Hour = " + CloseHour;
    }
}
