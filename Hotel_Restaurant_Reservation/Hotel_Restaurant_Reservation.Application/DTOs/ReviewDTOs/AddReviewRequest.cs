namespace Hotel_Restaurant_Reservation.Application.DTOs.ReviewDTOs;

public class AddReviewRequest
{
    public string Description { get; set; }

    public double CustomerStarRating { get; set; }

    public Guid RestaurantId { get; set; }

    public Guid CustomerId { get; set; }
}
