using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.UpdateRestaurantBooking;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RestaurantBookingProfile : Profile
{
    public RestaurantBookingProfile()
    {
        CreateMap<RestaurantBooking, RestaurantBookingResponse>();
        CreateMap<AddRestaurantBookingRequest, RestaurantBooking>();
        CreateMap<UpdateRestaurantBookingRequest, RestaurantBooking>();
    }
}