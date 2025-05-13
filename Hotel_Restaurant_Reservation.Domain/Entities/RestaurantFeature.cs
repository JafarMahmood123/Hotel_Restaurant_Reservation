namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantFeature
{
    // Key Properties

    public Guid Id { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid FeatureId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual Feature Feature { get; set; }

    public RestaurantFeature()
    {

    }
}
