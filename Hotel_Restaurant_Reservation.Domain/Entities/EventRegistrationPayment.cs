namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class EventRegistrationPayment
{
    public Guid Id { get; set; }
    public string OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public Guid EventRegistrationId { get; set; }
    public virtual EventRegistration EventRegistration { get; set; }
}