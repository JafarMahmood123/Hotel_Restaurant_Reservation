using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CityDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.MealTypeDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class MealTypeProfile : Profile
{

    public MealTypeProfile()
    {

        CreateMap<MealType, MealTypeRequest>();

        CreateMap<MealTypeRequest, MealType>();
           //.ForMember(dest => dest.Id, opt => opt.Ignore())
           //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           //.AfterMap(async (src, dest) =>
           //{
           //    var existingMealType = await genericRepository.GetFirstOrDefaultAsync(c => c.Name == src.Name);

           //    if (existingMealType != null)
           //    {
           //        dest.Id = existingMealType.Id;
           //    }

           //    dest.Id = Guid.NewGuid();
           //});
    }
}
