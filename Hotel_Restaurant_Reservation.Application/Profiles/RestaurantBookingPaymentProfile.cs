using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Payments.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RestaurantBookingPaymentProfile: Profile
{
    public RestaurantBookingPaymentProfile()
    {
        CreateMap<RestaurantBookingPayment, RestaurantBookingPaymentResponse>();
    }
}
