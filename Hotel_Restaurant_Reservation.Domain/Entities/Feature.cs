namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Feature
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurants { get; set; }

    public Feature()
    {
        
    }

    public override string ToString()
    {
        return "Feature = " + Name;
    }
}
