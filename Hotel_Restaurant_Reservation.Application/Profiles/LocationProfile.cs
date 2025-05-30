using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocationDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.LocationDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class LocationProfile : Profile
{

    public LocationProfile()
    {
        CreateMap<Location, LocationResponse>().ReverseMap();

        CreateMap<Location, AddLocationRequest>();

        CreateMap<AddLocationRequest, Location>();

        CreateMap<UpdateLocationRequest, Location>();
    }
}