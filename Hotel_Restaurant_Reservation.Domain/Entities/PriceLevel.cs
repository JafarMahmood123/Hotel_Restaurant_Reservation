namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class PriceLevel
{
    // Key Properties
    public Guid Id { get; set; }

    public int MinLevel { get; set; }

    public int MaxLevel { get; set; }

    // Foreign Keys

    public int RestaurantId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurants { get; set; }

    public PriceLevel()
    {
        
    }
}
