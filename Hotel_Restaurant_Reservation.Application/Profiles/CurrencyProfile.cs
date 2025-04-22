using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CurrencyType;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        CreateMap<CurrencyTypeRequest, CurrencyType>()
            .ForMember(dest => dest.CurrencyCode,
            opt => opt.MapFrom(src => src.Name));
    }
}
