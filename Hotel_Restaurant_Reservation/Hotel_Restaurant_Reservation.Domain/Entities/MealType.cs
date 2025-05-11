namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class MealType
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys


    // Navigation Properties

    public virtual ICollection<RestaurantMealType> RestaurantMealTypes { get; set; }

    public MealType()
    {
        RestaurantMealTypes = new HashSet<RestaurantMealType>();
    }
}
