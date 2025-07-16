namespace Hotel_Restaurant_Reservation.Application.Implementation.Payments.Queries;

public class PaymentResponse
{
    public Guid Id { get; set; }
    public string OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
}