using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.RoomType;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RoomTypeProfile : Profile
{
    public RoomTypeProfile()
    {
        CreateMap<RoomTypeRequest, RoomType>()
            .ForMember(dest => dest.Description,
            opt => opt.MapFrom(src => src.Description));
    }
}
