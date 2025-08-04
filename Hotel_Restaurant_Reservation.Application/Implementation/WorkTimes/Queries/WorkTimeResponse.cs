namespace Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;

public class WorkTimeResponse
{
    public Guid Id { get; set; }

    public string Day { get; set; }

    public TimeOnly OpenHour { get; set; }

    public TimeOnly CloseHour { get; set; }
}
