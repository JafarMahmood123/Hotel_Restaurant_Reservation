namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.UpdateHotel;

public class UpdateHotelRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Url { get; set; }

    public Guid LocationId { get; set; }
}
