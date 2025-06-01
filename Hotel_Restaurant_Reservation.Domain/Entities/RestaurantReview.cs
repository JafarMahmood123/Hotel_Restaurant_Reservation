namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantReview
{
    public Guid Id { get; set; }

    public Guid RestaurantId { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime ReviewDateTime { get; set; }

    public string Description { get; set; }

    public double CustomerStarRating { get; set; }

    public double CustomerServiceStarRating { get; set; }

    public double CustomerFoodStarRating { get; set; }

    public virtual Restaurant Restaurant { get; set; }

    public virtual Customer Customer { get; set; }

    public RestaurantReview()
    {
        
    }
}
