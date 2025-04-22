using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Country;
using Hotel_Restaurant_Reservation.Domain.Entities;
using System.Threading.Tasks.Sources;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryRequest, Country>()
            .ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.Name));
    }
}
