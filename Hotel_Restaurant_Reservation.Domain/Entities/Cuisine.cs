namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Cuisine
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys


    // Navigation Properties

    public virtual ICollection<RestaurantCuisine> RestaurantCuisines { get; set; }

    public Cuisine()
    {
        RestaurantCuisines = new HashSet<RestaurantCuisine>();
    }
}
