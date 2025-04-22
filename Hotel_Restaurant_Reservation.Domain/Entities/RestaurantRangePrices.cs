namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantRangePrices
{

    // Key Properties
    public Guid Id { get; set; }

    public double MinPrice { get; set; }

    public double MaxPrice { get; set; }

    // Foreign Keys

    public int RestaurantId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurants { get; set; }

    public RestaurantRangePrices()
    {
        
    }
}
