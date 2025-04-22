using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocation;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class LocalLocationProfile : Profile
{
    public LocalLocationProfile()
    {
        CreateMap<LocalLocationRequest, LocalLocation>()
            .ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.Name));
    }
}
