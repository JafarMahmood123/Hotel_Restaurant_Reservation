using PayPalCheckoutSdk.Orders;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Payment;

public interface IPayPalService
{
    Task<Order> CreateOrder(string totalAmount, string currencyCode);
    Task<Order> CaptureOrder(string orderId);
    Task<bool> VerifyWebhookSignature(string requestBody, WebhookHeaders headers);
}