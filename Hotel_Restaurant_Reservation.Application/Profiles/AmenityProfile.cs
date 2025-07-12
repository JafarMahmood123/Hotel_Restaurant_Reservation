using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.AddAmenity;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class AmenityProfile : Profile
{
    public AmenityProfile()
    {
        CreateMap<Amenity, AmenityResponse>();
        CreateMap<AddAmenityRequest, Amenity>();
    }
}