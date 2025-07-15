using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Commands.AddMealType;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
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
