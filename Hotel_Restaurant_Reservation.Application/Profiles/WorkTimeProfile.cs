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

        CreateMap<WorkTime, WorkTimeResponse>();
    }
}
