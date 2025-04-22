namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Review
{
    // Key Properties
    public Guid Id { get; set; }

    public string Description { get; set; }

    public bool IsLiked { get; set; }

    public double CustomerStarRating { get; set; }

    // Foreign Keys

    public int HotelId { get; set; }

    public int RestaurantId { get; set; }

    public int CustomerId { get; set; }

    // Navigation Properties

    public virtual Hotel? Hotel { get; set; }

    public virtual Restaurant? Restaurant { get; set; }

    public virtual Customer Customer { get; set; }

    public Review()
    {
        
    }
}
