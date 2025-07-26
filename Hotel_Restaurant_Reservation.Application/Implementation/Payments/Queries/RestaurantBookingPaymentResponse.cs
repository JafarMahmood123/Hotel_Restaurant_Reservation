using Hotel_Restaurant_Reservation.Domain.Enums;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Payments.Queries;

public class RestaurantBookingPaymentResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public Guid? CurrencyTypeId { get; set; }
    public PaymentStatus Status { get; set; }
    public Guid RestaurantBookingId { get; set; }
}
