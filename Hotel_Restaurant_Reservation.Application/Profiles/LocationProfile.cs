using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Location;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class LocationProfile : Profile
{

    public LocationProfile(IMapper mapper)
    {
        CreateMap<LocationRequest, Location>()
            .ForMember(dest => dest.Country,
            opt => opt.MapFrom(src => src.CountryRequest))
            .ForMember(dest => dest.City,
            opt => opt.MapFrom(src => src.CityRequest))
            .ForMember(dest => dest.LocalLocation,
            opt => opt.MapFrom(src => src.LocalLocationRequest));
    }
}
