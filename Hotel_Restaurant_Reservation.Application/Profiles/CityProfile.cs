using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.City;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CityRequest, City>()
            .ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.Name));
    }
}
