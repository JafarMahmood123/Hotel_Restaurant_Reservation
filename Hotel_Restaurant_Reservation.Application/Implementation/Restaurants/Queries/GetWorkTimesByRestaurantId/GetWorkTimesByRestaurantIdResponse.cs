namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetWorkTimesByRestaurantId;

public class GetWorkTimesByRestaurantIdResponse
{
    public Guid Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
}