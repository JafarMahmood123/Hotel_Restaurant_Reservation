using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CityDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class CityProfile : Profile
{

    public CityProfile()
    {

        CreateMap<City, AddCityRequest>();

        CreateMap<AddCityRequest, City>();

        CreateMap<City, CityResponse>();

        CreateMap<UpdateCityRequest,  City>();
    }
}
