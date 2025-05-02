namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Review
{
    // Key Properties
    public Guid Id { get; set; }

    public string Description { get; set; }

    public double CustomerStarRating { get; set; }

    // Foreign Keys

    // Navigation Properties

   public virtual ICollection<RestaurantReview> RestaurantReviews { get; set; }

    public Review()
    {
        
    }
}
