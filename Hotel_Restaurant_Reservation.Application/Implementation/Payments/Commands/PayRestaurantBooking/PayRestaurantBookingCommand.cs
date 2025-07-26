using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Payments.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Payments.Commands.PayRestaurantBooking
{
    public class PayRestaurantBookingCommand : ICommand<Result<RestaurantBookingPaymentResponse>>
    {
        public PayRestaurantBookingRequest Request { get; }

        public PayRestaurantBookingCommand(PayRestaurantBookingRequest request)
        {
            Request = request;
        }
    }
}