namespace Hotel_Restaurant_Reservation.Application.Abstractions.Payment
{
    public interface ILocalPaymentService
    {
        Task<Guid> CreateOrder(decimal totalAmount, string currencyCode);
        Task<bool> CaptureOrder(string orderId);
    }
}