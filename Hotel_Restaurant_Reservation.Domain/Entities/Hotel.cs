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


    // Check later after adding
    public int NumberOfRooms { get; set; }

    // Foreign Keys

    public int PropertyTypeId { get; set; }

    public int RoomId { get; set; }

    public int HotelRangePricesId { get; set; }

    public int CurrencyTypeId { get; set; }

    public int HotelReservationId { get; set; }

    public int ReviewId { get; set; }

    public int LocationId { get; set; }

    // Navigation Properties

    public virtual PropertyType PropertyType { get; set; }

    public virtual ICollection<Room> Rooms { get; set; }

    public virtual HotelRangePrices HotelRangePrices { get; set; }

    public virtual ICollection<CurrencyType> CurrencyType { get; set; }

    public virtual ICollection<HotelReservation> HotelReservations { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }

    public virtual Location Location { get; set; }

    public Hotel()
    {
        Rooms = new HashSet<Room>();
        CurrencyType = new HashSet<CurrencyType>();
        HotelReservations = new HashSet<HotelReservation>();
        Reviews = new HashSet<Review>();
    }
}
