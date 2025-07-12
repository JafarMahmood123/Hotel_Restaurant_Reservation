namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Amenity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<RoomAmenity> RoomAmenities { get; set; }

    public virtual ICollection<HotelAmenityPrice> HotelAmenitiesPrices { get; set; }

    public Amenity()
    {
        RoomAmenities = new HashSet<RoomAmenity>();
        HotelAmenitiesPrices = new HashSet<HotelAmenityPrice>(); 
    }
}
