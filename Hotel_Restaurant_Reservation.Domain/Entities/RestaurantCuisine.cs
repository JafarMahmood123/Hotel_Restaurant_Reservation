namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantCuisine
{
    // Key Properties

    public Guid Id { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid CuisineId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual Cuisine Cuisine { get; set; }

    public RestaurantCuisine()
    {

    }
}
