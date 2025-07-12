using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocationDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.UpdateLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries;
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