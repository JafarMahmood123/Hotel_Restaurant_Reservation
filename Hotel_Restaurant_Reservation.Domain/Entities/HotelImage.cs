namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class HotelImage
{
    public Guid Id { get; set; }

    public string Url { get; set; }

    public Guid HotelId { get; set; }

    public virtual Hotel Hotel { get; set; }
}
