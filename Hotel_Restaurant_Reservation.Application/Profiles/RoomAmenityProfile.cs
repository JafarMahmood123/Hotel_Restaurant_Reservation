using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.RoomAmenity;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RoomAmenityProfile : Profile
{
    public RoomAmenityProfile()
    {
        CreateMap<RoomAmenityRequest, RoomAmenity>()
            .ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.Name));
    }
}
