using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CountryDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class DishProfile : Profile
{

    public DishProfile()
    {;

        CreateMap<Dish, DishRequest>();

        CreateMap<CountryRequest, Country>();
        //.ForMember(dest => dest.Id, opt => opt.Ignore())
        //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //.AfterMap(async (src, dest) =>
        //{
        //    var existingDish = await genericRepository.GetFirstOrDefaultAsync(c => c.Name == src.Name);

        //    if (existingDish != null)
        //    {
        //        dest.Id = existingDish.Id;
        //    }

        //    dest.Id = Guid.NewGuid();
        //});
    }
}