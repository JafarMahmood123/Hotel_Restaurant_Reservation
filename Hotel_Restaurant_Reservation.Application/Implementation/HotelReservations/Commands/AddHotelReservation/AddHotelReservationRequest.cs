namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.AddHotelReservation;

public class AddHotelReservationRequest
{
    public Guid HotelId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid RoomId { get; set; }
    public int NumberOfPeople { get; set; }
    public DateOnly ReceivationStartDate { get; set; }
    public DateOnly ReceivationEndDate { get; set; }
}