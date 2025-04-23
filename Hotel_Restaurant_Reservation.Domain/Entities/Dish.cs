namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Dish
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurants { get; set; }

    public Dish()
    {
        
    }

    public override string ToString()
    {
        return "Name = " + Name + ", Price = " + Price;
    }
}
