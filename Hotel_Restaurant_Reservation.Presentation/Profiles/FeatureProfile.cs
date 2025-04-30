using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CountryDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class FeatureProfile : Profile
{

    public FeatureProfile()
    {

        CreateMap<Feature, FeatureRequest>();

        CreateMap<FeatureRequest, Feature>();
        //.ForMember(dest => dest.Id, opt => opt.Ignore())
        //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //.AfterMap(async (src, dest) =>
        //{
        //    var existingFeature = await cityRepository.GetFirstOrDefaultAsync(c => c.Name == src.Name);

        //    if (existingFeature != null)
        //    {
        //        dest.Id = existingFeature.Id;
        //    }

        //    dest.Id = Guid.NewGuid();
        //});
    }
}
