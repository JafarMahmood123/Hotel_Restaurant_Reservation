namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Dish
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? PictureUrl { get; set; }

    public virtual ICollection<RestaurantDishPrice> RestaurantDishPrice { get; set; }

    public Dish()
    {
        RestaurantDishPrice = new HashSet<RestaurantDishPrice>();
    }
}
