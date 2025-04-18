using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class HotelResponseProfile : Profile
{
    public HotelResponseProfile()
    {
        CreateMap<Hotel, HotelResponse>().ReverseMap();
    }
}
