using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Commands.AddDish;
using Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class DishProfile : Profile
{

    public DishProfile()
    {;

        CreateMap<Dish, DishResponse>();

        CreateMap<AddDishRequest, Dish>();
    }
}