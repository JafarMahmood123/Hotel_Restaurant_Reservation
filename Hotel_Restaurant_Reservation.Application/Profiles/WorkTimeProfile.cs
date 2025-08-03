using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.WorkTimeDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetWorkTimesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class WorkTimeProfile : Profile
{

    public WorkTimeProfile()
    {
        CreateMap<AddWorkTimeRequest, WorkTime>();

        CreateMap<WorkTime, WorkTimeResponse>();

        // Update this mapping configuration
        CreateMap<WorkTime, GetWorkTimesByRestaurantIdResponse>()
            .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => Enum.Parse<DayOfWeek>(src.Day)))
            .ForMember(dest => dest.OpeningTime, opt => opt.MapFrom(src => src.OpenHour))
            .ForMember(dest => dest.ClosingTime, opt => opt.MapFrom(src => src.CloseHour));
    }
}