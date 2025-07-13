using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RestaurantProfile : Profile
{

    public RestaurantProfile()
    {

        CreateMap<Restaurant, RestaurantResponse>();

        CreateMap<AddRestaurantRequest, Restaurant>();

        CreateMap<RestaurantUpdateRequest, Restaurant>();
    }
}
