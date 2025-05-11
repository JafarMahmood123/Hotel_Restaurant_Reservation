using Hotel_Restaurant_Reservation.Application.DTOs.LocationDTOs;

namespace Hotel_Restaurant_Reservation.Application.DTOs.RestaurantDTOs;

public class RestaurantAddRequest
{

    public string Name { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public string PictureUrl { get; set; }

    public double StarRating { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int NumberOfTables { get; set; }

    public string PriceLevel { get; set; }

    public AddLocationRequest addLocationRequest { get; set; }
}
