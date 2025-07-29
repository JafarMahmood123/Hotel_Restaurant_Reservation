namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAmenitiesByHotelId;

public class GetAmenitiesByHotelIdResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public Guid HotelId { get; set; }
}
