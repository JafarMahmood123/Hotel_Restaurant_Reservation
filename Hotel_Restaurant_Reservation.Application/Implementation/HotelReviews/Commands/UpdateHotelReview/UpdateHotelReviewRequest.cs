namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.UpdateHotelReview;

public class UpdateHotelReviewRequest
{
    public string Description { get; set; }
    public double OverallRating { get; set; }
    public double ServiceRating { get; set; }
    public double CleanlinessRating { get; set; }
    public double ValueRating { get; set; }
}