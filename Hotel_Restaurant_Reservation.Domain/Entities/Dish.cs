namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Dish
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }


    // Foreign Keys



    // Navigation Properties

    public virtual ICollection<RestaurantDishPrice> RestaurantDishPrice { get; set; }

    public Dish()
    {
        RestaurantDishPrice = new HashSet<RestaurantDishPrice>();
    }
}
