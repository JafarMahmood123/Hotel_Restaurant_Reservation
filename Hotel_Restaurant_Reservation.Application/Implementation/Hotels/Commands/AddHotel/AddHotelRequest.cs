namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;

public class AddHotelRequest
{
    // Key Properties

    public string Name { get; set; }

    public string Description { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Url { get; set; }

    public double StarRate { get; set; }

    public int NumberOfRooms { get; set; }

    public Guid LocationId { get; set; }

    // Additional Keys

    //public PropertyTypeRequest PropertyType { get; set; }

    //public IEnumerable<RoomRequest> Rooms { get; set; }

    //public HotelRangePricesRequest HotelRangePrices { get; set; }

    //public CurrencyTypeRequest CurrencyType { get; set; }

    //public LocationRequest Location { get; set; }
}
