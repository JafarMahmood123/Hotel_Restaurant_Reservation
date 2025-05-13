namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantMealType
{
    // Key Properties

    public Guid Id { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid MealTypeId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual MealType MealType { get; set; }

    public RestaurantMealType()
    {

    }
}
