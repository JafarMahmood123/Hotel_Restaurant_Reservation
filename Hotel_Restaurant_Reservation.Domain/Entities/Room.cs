namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Room
{
    // Key Properties
    public Guid Id { get; set; }

    public int MaxOccupancy { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    // Foreign Keys

    public Guid HotelId { get; set; }

    public Guid HotelReservationId { get; set; }

    // Navigation Properties

    public virtual Hotel Hotel { get; set; }

    public virtual ICollection<HotelReservation> HotelReservations { get; set; }

    public Room()
    {
        HotelReservations = new HashSet<HotelReservation>();
    }
}
