using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CuisineDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class CuisineProfile : Profile
{

    public CuisineProfile()
    {

        CreateMap<Cuisine, CuisineRequest>();

        CreateMap<CuisineRequest, Cuisine>();
           //.ForMember(dest => dest.Id, opt => opt.Ignore())
           //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           //.AfterMap(async (src, dest) =>
           //{
           //    var existingCuisine = await genericRepository.GetFirstOrDefaultAsync(c => c.Name == src.Name);

           //    if (existingCuisine != null)
           //    {
           //        dest.Id = existingCuisine.Id;
           //    }

           //    dest.Id = Guid.NewGuid();
           //});
    }
}
