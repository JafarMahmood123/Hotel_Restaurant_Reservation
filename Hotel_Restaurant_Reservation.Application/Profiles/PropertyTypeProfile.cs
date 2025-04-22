using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.PropertyType;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class PropertyTypeProfile : Profile
{
    public PropertyTypeProfile()
    {
        CreateMap<PropertyTypeRequest, PropertyType>()
            .ForMember(dest => dest.Name,
            opt => opt.MapFrom(dest => dest.Name));
    }
}
