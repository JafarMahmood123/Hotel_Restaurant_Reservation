using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Payments.Commands.AddPayment;
using Hotel_Restaurant_Reservation.Application.Implementation.Payments.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<AddPaymentRequest, Payment>();
        CreateMap<Payment, PaymentResponse>();
    }
}