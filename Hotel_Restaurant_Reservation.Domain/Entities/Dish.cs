namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Dish
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<RestaurantDish> RestaurantDishPrice { get; set; }

    public Dish()
    {
        RestaurantDishPrice = new HashSet<RestaurantDish>();
    }
}
