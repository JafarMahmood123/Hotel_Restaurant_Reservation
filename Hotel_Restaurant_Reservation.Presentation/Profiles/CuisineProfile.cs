using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CuisineDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class CuisineProfile : Profile
{

    public CuisineProfile()
    {

        CreateMap<Cuisine, AddCuisineRequest>();

        CreateMap<AddCuisineRequest, Cuisine>();
           
        CreateMap<AddCuisineToRestaurantRequest, Cuisine>();

        CreateMap<Cuisine, CuisineResponse>();
    }
}
