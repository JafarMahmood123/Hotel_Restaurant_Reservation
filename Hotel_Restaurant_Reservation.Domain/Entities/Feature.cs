namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Feature
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys


    // Navigation Properties

    public virtual ICollection<RestaurantFeature> RestaurantFeatures { get; set; }

    public Feature()
    {
        RestaurantFeatures = new HashSet<RestaurantFeature>();
    }
}
