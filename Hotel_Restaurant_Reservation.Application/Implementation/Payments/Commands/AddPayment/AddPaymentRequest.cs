namespace Hotel_Restaurant_Reservation.Application.Implementation.Payments.Commands.AddPayment;

public class AddPaymentRequest
{
    public string OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}