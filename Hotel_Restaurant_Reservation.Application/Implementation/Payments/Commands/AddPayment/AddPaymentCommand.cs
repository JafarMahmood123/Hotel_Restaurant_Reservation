using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Payments.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Payments.Commands.AddPayment;

public class AddPaymentCommand : ICommand<Result<PaymentResponse>>
{
    public AddPaymentRequest AddPaymentRequest { get; }

    public AddPaymentCommand(AddPaymentRequest addPaymentRequest)
    {
        AddPaymentRequest = addPaymentRequest;
    }
}