namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class HotelReservation
{
    public Guid Id { get; set; }

    public int RoomNumber { get; set; }

    public DateTime ReservationDateTime { get; set; }

    public DateOnly ReceiveDate { get; set; }

    public int NumberOfPeople { get; set; }
}
