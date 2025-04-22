using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.HotelRangePrices;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class HotelRangePricesProfile : Profile
{
    public HotelRangePricesProfile()
    {
        CreateMap<HotelRangePricesRequest, HotelRangePrices>()
            .ForMember(dest => dest.MinPrice,
            opt => opt.MapFrom(src => src.MinPrice))
            .ForMember(dest => dest.MaxPrice,
            opt => opt.MapFrom(src => src.MaxPrice));
    }
}
