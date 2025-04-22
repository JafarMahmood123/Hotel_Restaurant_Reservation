using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<Hotel, HotelResponse>().ReverseMap();

        CreateMap<HotelAddRequest, Hotel>()
            .ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.Name))

            .ForMember(dest => dest.Latitude,
            opt => opt.MapFrom(src => src.Latitude))

            .ForMember(dest => dest.Longitude,
            opt => opt.MapFrom(src => src.Longitude))

            .ForMember(dest => dest.Url,
            opt => opt.MapFrom(src => src.Url))

            .ForMember(dest => dest.StarRate,
            opt => opt.MapFrom(src => src.StarRate))

            .ForMember(dest => dest.NumberOfRooms,
            opt => opt.MapFrom(src => src.NumberOfRooms))

            .ForMember(dest => dest.Location,
            opt => opt.MapFrom(src => src.Location))

            .ForMember(dest => dest.PropertyType,
            opt => opt.MapFrom(src => src.PropertyType))

            .ForMember(dest => dest.Rooms,
            opt => opt.MapFrom(src => src.Rooms))

            .ForMember(dest => dest.HotelRangePrices,
            opt => opt.MapFrom(src => src.HotelRangePrices))

            .ForMember(dest => dest.CurrencyType,
            opt => opt.MapFrom(src => src.CurrencyType));
    }
}
