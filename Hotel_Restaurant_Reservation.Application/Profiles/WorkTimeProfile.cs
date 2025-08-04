using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddWorkTimesToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetWorkTimesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using System;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class WorkTimeProfile : Profile
{
    public WorkTimeProfile()
    {
        CreateMap<AddWorkTimesToRestaurantRequest, RestaurantWorkTime>();

        // CORRECTED MAPPING:
        // The mapping for WorkTimeResponse is now explicit, just like the one
        // for GetWorkTimesByRestaurantIdResponse. This ensures the string 'Day' is
        // correctly parsed into the 'DayOfWeek' enum and property names are matched.
        CreateMap<RestaurantWorkTime, WorkTimeResponse>()
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day)) // Added 'true' to ignore case
            .ForMember(dest => dest.OpenHour, opt => opt.MapFrom(src => src.OpenHour))
            .ForMember(dest => dest.CloseHour, opt => opt.MapFrom(src => src.CloseHour));

        //// This mapping was already correct and remains the same.
        //CreateMap<RestaurantWorkTime, GetWorkTimesByRestaurantIdResponse>()
        //    .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => Enum.Parse<DayOfWeek>(src.Day, true))) // Added 'true' to ignore case
        //    .ForMember(dest => dest.OpeningTime, opt => opt.MapFrom(src => src.OpenHour))
        //    .ForMember(dest => dest.ClosingTime, opt => opt.MapFrom(src => src.CloseHour));
    }
}
