namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;

public class HotelResponse
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Url { get; set; }

    public double StarRate { get; set; }

    public int NumberOfRooms { get; set; }

    public string PictureUrl { get; set; }
}
