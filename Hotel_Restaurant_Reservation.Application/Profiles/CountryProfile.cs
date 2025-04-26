using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CountryDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class CountryProfile : Profile
{

    public CountryProfile()
    {

        CreateMap<Country, CountryRequest>();

        CreateMap<CountryRequest, Country>();
            //.ForMember(dest => dest.Id, opt => opt.Ignore())
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.AfterMap(async (src, dest) =>
            //{
            //    var existingCountry = await genericRepository.GetFirstOrDefaultAsync(c => c.Name == src.Name);

            //    if (existingCountry != null)
            //    {
            //        dest.Id = existingCountry.Id;
            //    }

            //    dest.Id = Guid.NewGuid();
            //});
    }
}
