using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishResponse>();
    }
}
