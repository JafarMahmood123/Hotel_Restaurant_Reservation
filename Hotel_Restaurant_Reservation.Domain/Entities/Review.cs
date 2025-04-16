namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Review
{
    // Key Properties
    public Guid Id { get; set; }

    public string Description { get; set; }

    // Foreign Keys

    public Guid HotelId { get; set; }

    public Guid RestaurantId { get; set; }

    public Guid CustomerId { get; set; }

    // Navigation Properties

    public virtual Hotel? Hotel { get; set; }

    public virtual Restaurant? Restaurant { get; set; }

    public virtual Customer Customer { get; set; }

    public Review()
    {
        
    }
}
