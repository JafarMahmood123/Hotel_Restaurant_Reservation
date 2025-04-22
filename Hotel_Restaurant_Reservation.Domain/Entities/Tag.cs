namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Tag
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public int RestaurantId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurants { get; set; }

    public Tag()
    {
        
    }
}
