using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Restaurant;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RestaurantProfile : Profile
{

    public RestaurantProfile()
    {

        CreateMap<Restaurant, RestaurantResponse>().ReverseMap();

        CreateMap<RestaurantAddRequest, Restaurant>();
    }
}
