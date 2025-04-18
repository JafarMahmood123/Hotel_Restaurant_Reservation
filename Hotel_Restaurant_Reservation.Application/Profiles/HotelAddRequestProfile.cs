using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class HotelAddRequestProfile : Profile
{
    public HotelAddRequestProfile()
    {
        CreateMap<Hotel, HotelAddRequest>().ReverseMap();
            //.ForMember(dest => dest.PropertyType.Name, opt=>opt.MapFrom(src=>src.PropertyType.Name))
            //.ForMember(dest=>dest.Rooms,opt=>opt.MapFrom(src=>src.Rooms))
            
    }
}
