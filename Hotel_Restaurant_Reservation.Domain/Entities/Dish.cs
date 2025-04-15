namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Dish
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    // Navigation Properties

    public virtual ICollection<Restaurant> Restaurants { get; set; }

    public Dish()
    {
        Restaurants = new HashSet<Restaurant>();
    }
}
