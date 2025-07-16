namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class HotelReservation
{

    // Key Properties
    public Guid Id { get; set; }

    public DateTime ReservationDateTime { get; set; }

    public DateOnly ReceivationStartDate { get; set; }

    public DateOnly ReceivationEndDate { get; set; }

    public int NumberOfPeople { get; set; }

    // Foreign Keys

    public Guid HotelId { get; set; }

    public Guid UserId { get; set; }

    public Guid RoomId { get; set; }    

    // Navigation Properties

    public virtual Hotel Hotel { get; set; }

    public virtual User User { get; set; }

    public virtual Room Room { get; set; }

    public virtual HotelReservationPayment HotelReservationPayment { get; set; } 
}
