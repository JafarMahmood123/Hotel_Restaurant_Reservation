namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Hotel
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Url { get; set; }

    public double StarRate { get; set; }

    public int NumberOfRooms { get; set; }

    public double MinPrice { get; set; }

    public double MaxPrice { get; set; }    

    // Foreign Keys

    public Guid? PropertyTypeId { get; set; }

    public Guid? LocationId { get; set; }

    // Navigation Properties

    public virtual PropertyType PropertyType { get; set; }

    public virtual ICollection<Room> Rooms { get; set; }

    public virtual ICollection<HotelCurrencyType> HotelCurrencyTypes { get; set; }

    public virtual ICollection<HotelReservation> HotelReservations { get; set; }

    public virtual ICollection<HotelAmenityPrice> HotelAmenitiesPrices { get; set; }

    public virtual ICollection<HotelReview> HotelReviews { get; set; }

    public virtual Location Location { get; set; }

    public Hotel()
    {
        Rooms = new HashSet<Room>();
        CurrencyType = new HashSet<CurrencyType>();
        HotelReservations = new HashSet<HotelReservation>();
        HotelAmenitiesPrices = new HashSet<HotelAmenityPrice>();
        HotelReviews = new HashSet<HotelReview>();
    }
}
