namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public string PictureUrl { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int NumberOfTables { get; set; }

    // Foreign Keys

    public Guid LocationId { get; set; }
}
