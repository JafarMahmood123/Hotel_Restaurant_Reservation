namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries;

public class RestaurantReviewResponse
{
    public Guid Id { get; set; }

    public Guid RestaurantId { get; set; }

    public Guid CustomerId { get; set; }

    public string Description { get; set; }

    public double CustomerStarRating { get; set; }

    public double CustomerServiceStarRating { get; set; }

    public double CustomerFoodStarRating { get; set; }

}
