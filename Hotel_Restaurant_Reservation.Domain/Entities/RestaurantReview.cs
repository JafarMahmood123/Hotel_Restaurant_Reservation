namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantReview
{
    public Guid Id { get; set; }

    public Guid RestaurantId { get; set; }

    public Guid ReviewId { get; set; }

    public Guid CustomerId { get; set; }

    public virtual Restaurant Restaurant { get; set; }

    public virtual Review Review { get; set; }

    public virtual Customer Customer { get; set; }

    public RestaurantReview()
    {
        
    }
}
