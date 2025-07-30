namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;

public class HotelReservationResponse
{
    public Guid Id { get; set; }
    public DateTime ReservationDateTime { get; set; }
    public DateOnly ReceivationStartDate { get; set; }
    public DateOnly ReceivationEndDate { get; set; }
    public int NumberOfPeople { get; set; }
    public Guid HotelId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid RoomId { get; set; }
}