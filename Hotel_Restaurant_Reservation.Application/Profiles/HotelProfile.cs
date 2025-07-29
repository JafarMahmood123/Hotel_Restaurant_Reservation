using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.UpdateHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAmenitiesByHotelId;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<Hotel, HotelResponse>();

        CreateMap<AddHotelRequest, Hotel>();

        CreateMap<UpdateHotelRequest, Hotel>();

        CreateMap<HotelAmenityPrice, GetAmenitiesByHotelIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AmenityId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Amenity.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
    }
}
