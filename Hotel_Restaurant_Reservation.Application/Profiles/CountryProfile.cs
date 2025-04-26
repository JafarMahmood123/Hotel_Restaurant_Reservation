using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CountryDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class CountryProfile : Profile
{

    public CountryProfile()
    {
        CreateMap<Country, CountryResponse>().ReverseMap();

        CreateMap<Country, AddCountryRequest>();

        CreateMap<AddCountryRequest, Country>();

        CreateMap<UpdateCountryRequest, Country>();
    }
}
