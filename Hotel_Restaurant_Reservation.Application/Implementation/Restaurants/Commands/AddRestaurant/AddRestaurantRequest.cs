using Hotel_Restaurant_Reservation.Application.DTOs.LocationDTOs;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;

public class AddRestaurantRequest
{

    public string Name { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public string PictureUrl { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int NumberOfTables { get; set; }

    public AddLocationRequest addLocationRequest { get; set; }
}
