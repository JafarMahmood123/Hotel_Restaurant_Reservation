using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.WorkTimeDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class WorkTimeProfile : Profile
{

    public WorkTimeProfile()
    {
        CreateMap<AddWorkTimeRequest, WorkTime>();

        CreateMap<WorkTime, WorkTimeResponse>();
    }
}
