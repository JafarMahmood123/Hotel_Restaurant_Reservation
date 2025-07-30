using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.AddHotelReservation;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.UpdateHotelReservation;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class HotelReservationProfile : Profile
{
    public HotelReservationProfile()
    {
        CreateMap<AddHotelReservationRequest, HotelReservation>();
        CreateMap<HotelReservation, HotelReservationResponse>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.UserId));
        CreateMap<UpdateHotelReservationRequest, HotelReservation>();
    }
}