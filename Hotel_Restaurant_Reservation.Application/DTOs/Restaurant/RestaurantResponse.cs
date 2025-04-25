using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.DTOs.Restaurant;

public class RestaurantResponse
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public string PictureUrl { get; set; }

    public double StarRating { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int NumberOfTables { get; set; }

    public string PriceLevel { get; set; }

    public double MinPrice { get; set; }

    public double MaxPrice { get; set; }

    // Foreign Keys

    public Guid LocationId { get; set; }

}
