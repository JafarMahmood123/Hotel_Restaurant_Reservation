using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.MealTypeDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Commands;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class MealTypeProfile : Profile
{

    public MealTypeProfile()
    {

        CreateMap<MealType, MealTypeResponse>();

        CreateMap<AddMealTypeRequest, MealType>();
    }
}
