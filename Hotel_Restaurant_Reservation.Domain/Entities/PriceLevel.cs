namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class PriceLevel
{
    // Key Properties
    public Guid Id { get; set; }

    public int Level { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    // Navigation Properties

    public virtual ICollection<Restaurant> Restaurants { get; set; }

    public PriceLevel()
    {
        Restaurants = new HashSet<Restaurant>();
    }
}
