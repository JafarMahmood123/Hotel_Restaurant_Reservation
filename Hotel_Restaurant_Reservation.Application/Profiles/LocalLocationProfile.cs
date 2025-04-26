using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocationDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class LocalLocationProfile : Profile
{

    public LocalLocationProfile()
    {

        CreateMap<LocalLocation, LocalLocationRequest>();

        CreateMap<LocalLocationRequest, LocalLocation>();
           //.ForMember(dest => dest.Id, opt => opt.Ignore())
           //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           //.AfterMap(async (src, dest) =>
           //{
           //    var existingLocalLocation = await genericRepository.GetFirstOrDefaultAsync(c => c.Name == src.Name);

           //    if (existingLocalLocation != null)
           //    {
           //        dest.Id = existingLocalLocation.Id;
           //    }

           //    dest.Id = Guid.NewGuid();
           //});
    }
}
