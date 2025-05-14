namespace Hotel_Restaurant_Reservation.Application.DTOs.WorkTimeDTOs;

public class WorkTimeResponse
{
    public Guid Id { get; set; }

    public DayOfWeek Day { get; set; }

    public TimeOnly OpenHour { get; set; }

    public TimeOnly CloseHour { get; set; }
}
