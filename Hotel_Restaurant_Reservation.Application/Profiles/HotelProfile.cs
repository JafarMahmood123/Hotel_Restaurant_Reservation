using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Application.DTOs.HotelDTOs;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<Hotel, HotelResponse>().ReverseMap();

        CreateMap<HotelAddRequest, Hotel>();
    }
}
