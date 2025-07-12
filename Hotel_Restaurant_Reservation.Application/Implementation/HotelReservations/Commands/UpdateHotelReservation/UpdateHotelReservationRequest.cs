namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.UpdateHotelReservation;

public class UpdateHotelReservationRequest
{
    public int NumberOfPeople { get; set; }
    public DateOnly ReceivationStartDate { get; set; }
    public DateOnly ReceivationEndDate { get; set; }
}