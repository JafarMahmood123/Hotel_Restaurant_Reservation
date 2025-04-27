using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocationDTOs;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class LocalLocationProfile : Profile
{

    public LocalLocationProfile()
    {
        CreateMap<LocalLocation, LocalLocationResponse>().ReverseMap();

        CreateMap<UpdateLocalLocationRequest, LocalLocation>();

        CreateMap<LocalLocation, AddLocalLocationRequest>();

        CreateMap<AddLocalLocationRequest, LocalLocation>();
    }
}
