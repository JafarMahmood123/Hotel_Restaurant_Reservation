namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantManager
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<Restaurant> Restaurants { get; set; }
}
