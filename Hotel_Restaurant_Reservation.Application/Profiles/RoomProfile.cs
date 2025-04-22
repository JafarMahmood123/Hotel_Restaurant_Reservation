using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Room;
using Hotel_Restaurant_Reservation.Domain.Entities;
using System.Xml.Serialization;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<RoomRequest, Room>()
            .ForMember(dest => dest.MaxOccupancy,
            opt => opt.MapFrom(src => src.MaxOccupancy))
            .ForMember(dest => dest.RoomType,
            opt => opt.MapFrom(src => src.RoomTypeRequest))
            .ForMember(dest => dest.RoomAmenities,
            opt => opt.MapFrom(src => src.RoomAmenityRequests))
            .ForMember(dest => dest.Description,
            opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price,
            opt => opt.MapFrom(src => src.Price));
    }
}
