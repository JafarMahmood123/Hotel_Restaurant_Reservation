using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.MealTypeDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.TagDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class TagProfile : Profile
{

    public TagProfile()
    {

        CreateMap<Tag, TagRequest>();

        CreateMap<TagRequest, Tag>();
           //.ForMember(dest => dest.Id, opt => opt.Ignore())
           //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           //.AfterMap(async (src, dest) =>
           //{
           //    var existingTag = await cityRepository.GetFirstOrDefaultAsync(c => c.Name == src.Name);

           //    if (existingTag != null)
           //    {
           //        dest.Id = existingTag.Id;
           //    }

           //    dest.Id = Guid.NewGuid();
           //});
    }
}
