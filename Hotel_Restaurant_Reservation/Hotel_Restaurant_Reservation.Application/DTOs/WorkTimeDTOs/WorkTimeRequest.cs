namespace Hotel_Restaurant_Reservation.Application.DTOs.WorkTimeDTOs;

public class WorkTimeRequest
{
    public DayOfWeek Day { get; set; }

    public TimeOnly OpenHour { get; set; }

    public TimeOnly CloseHour { get; set; }
}
