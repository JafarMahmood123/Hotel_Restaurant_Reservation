namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;

public class AddRestaurantReviewRequest
{
    public Guid RestaurantId { get; set; }

    public Guid CustomerId { get; set; }

    public string Description { get; set; }

    public double CustomerStarRating { get; set; }

    public double CustomerServiceStarRating { get; set; }

    public double CustomerFoodStarRating { get; set; }
}
