namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class HotelReservation
{

    // Key Properties
    public Guid Id { get; set; }

    public int RoomNumber { get; set; }

    public DateTime ReservationDateTime { get; set; }

    public DateOnly ReceivationStartDate { get; set; }

    public DateOnly ReceivationEndDate { get; set; }

    public int NumberOfPeople { get; set; }

    // Foreign Keys

    public int HotelId { get; set; }

    public int CustomerId { get; set; }

    public int RoomId { get; set; }    

    // Navigation Properties

    public virtual Hotel Hotel { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual Room Room { get; set; }
}
