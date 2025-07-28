using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.UpdateRestaurantBooking;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using System.Xml.Serialization;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RestaurantBookingProfile : Profile
{
    public RestaurantBookingProfile()
    {
        CreateMap<RestaurantBooking, RestaurantBookingResponse>()
            .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name));
        CreateMap<AddRestaurantBookingRequest, RestaurantBooking>();
        CreateMap<UpdateRestaurantBookingRequest, RestaurantBooking>();
    }
}