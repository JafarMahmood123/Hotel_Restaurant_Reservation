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

    public Guid HotelId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid RoomId { get; set; }    

    // Navigation Properties

    public virtual Hotel Hotel { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual Room Room { get; set; }
}
