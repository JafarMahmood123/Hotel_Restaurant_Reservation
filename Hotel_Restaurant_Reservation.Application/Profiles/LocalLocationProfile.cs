using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocations;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.UpdateLocalLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class LocalLocationProfile : Profile
{
    public LocalLocationProfile()
    {
        CreateMap<LocalLocation, LocalLocationResponse>().ReverseMap();
        CreateMap<UpdateLocalLocationRequest, LocalLocation>();
        CreateMap<AddLocalLocationRequest, LocalLocation>();
    }
}