namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class MealType
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurants { get; set; }

    public MealType()
    {
        
    }

    public override string ToString()
    {
        return "Name = " + Name;
    }
}
