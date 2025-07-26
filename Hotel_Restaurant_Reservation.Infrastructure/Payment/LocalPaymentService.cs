using System;
using System.Threading.Tasks;
using Hotel_Restaurant_Reservation.Application.Abstractions.Payment;

namespace Hotel_Restaurant_Reservation.Infrastructure.Payment
{
    public class LocalPaymentService : ILocalPaymentService
    {
        public Task<Guid> CreateOrder(decimal totalAmount, string currencyCode)
        {
            var orderId = Guid.NewGuid();
            return Task.FromResult(orderId);
        }

        public Task<bool> CaptureOrder(string orderId)
        {
            Console.WriteLine($"Capturing local payment for order {orderId}");
            return Task.FromResult(true);
        }
    }
}