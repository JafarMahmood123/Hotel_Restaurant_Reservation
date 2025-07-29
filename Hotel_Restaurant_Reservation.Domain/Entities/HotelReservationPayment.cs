using Hotel_Restaurant_Reservation.Domain.Enums;

namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class HotelReservationPayment
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
    public PaymentStatus Status { get; set; }
    public Guid HotelReservationId { get; set; }
    public virtual HotelReservation HotelReservation { get; set; }
}