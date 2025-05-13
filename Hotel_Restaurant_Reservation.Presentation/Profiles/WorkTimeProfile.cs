using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.TagDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.WorkTimeDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class WorkTimeProfile : Profile
{

    public WorkTimeProfile()
    {

        CreateMap<WorkTime, WorkTimeRequest>();

        CreateMap<WorkTimeRequest, WorkTime>();
           //.ForMember(dest => dest.Id, opt => opt.Ignore())
           //.ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day))
           //.ForMember(dest => dest.OpenHour, opt => opt.MapFrom(src => src.OpenHour))
           //.ForMember(dest => dest.CloseHour, opt => opt.MapFrom(src => src.CloseHour))
           //.AfterMap(async (src, dest) =>
           //{
           //    var existingWorkTime = await cityRepository.GetFirstOrDefaultAsync(x => x.Day == dest.Day
           //    && x.OpenHour == dest.OpenHour && x.CloseHour == dest.CloseHour);

           //    if (existingWorkTime != null)
           //    {
           //        dest.Id = existingWorkTime.Id;
           //    }

           //    dest.Id = Guid.NewGuid();
           //});
    }
}
