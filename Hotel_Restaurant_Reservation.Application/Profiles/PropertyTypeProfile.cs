using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.AddPropertyType;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.UpdatePropertyType;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class PropertyTypeProfile : Profile
{
    public PropertyTypeProfile()
    {
        CreateMap<UpdatePropertyTypeRequest, PropertyType>();

        CreateMap<AddPropertyTypeRequest, PropertyType>();
    }
}