namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantDishesByRestaurantId;

public class RestaurantDishResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public string Description { get; set; }

    public string PictureUrl { get; set; }

    public Guid RestaurantId { get; set; }
}
