namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Tag
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys


    // Navigation Properties

    public virtual ICollection<RestaurantTag> RestaurantTags { get; set; }

    public Tag()
    {
        RestaurantTags = new HashSet<RestaurantTag>();
    }
}
