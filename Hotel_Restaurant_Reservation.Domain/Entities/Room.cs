namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Room
{
    // Key Properties
    public Guid Id { get; set; }

    public int MaxOccupancy { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    // Foreign Keys

    public int HotelId { get; set; }

    public int HotelReservationId { get; set; }

    public int RoomAmenitiesId { get; set; }

    public int RoomTypeId { get; set; }



    // Navigation Properties

    public virtual Hotel Hotel { get; set; }

    public virtual ICollection<HotelReservation> HotelReservations { get; set; }

    public virtual ICollection<RoomAmenity> RoomAmenities { get; set; }

    public virtual RoomType RoomType { get; set; }

    public Room()
    {
        HotelReservations = new HashSet<HotelReservation>();

        RoomAmenities = new HashSet<RoomAmenity>();
    }
}
