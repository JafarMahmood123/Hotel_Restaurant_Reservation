using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocationDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.LocationDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class LocationProfile : Profile
{

    public LocationProfile()
    {

        CreateMap<Location, LocationRequest>();

        CreateMap<LocationRequest, Location>();
           //.ForMember(dest => dest.Id, opt => opt.Ignore())
           //.AfterMap(async (src, dest) =>
           //{
           //    var existingLocation = await genericRepository.GetFirstOrDefaultAsync(x => x.CountryId == countryId
           //    && x.CityId == cityId && x.LocalLocationId == localLocationId);

           //    if (existingLocation != null)
           //    {
           //        dest.Id = existingLocation.Id;
           //    }

           //    dest.Id = Guid.NewGuid();
           //});
    }
}