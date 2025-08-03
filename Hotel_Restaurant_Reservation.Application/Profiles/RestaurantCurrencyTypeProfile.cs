using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantCuisinesByRestaurantId;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RestaurantCurrencyTypeProfile : Profile
{
    public RestaurantCurrencyTypeProfile()
    {
        CreateMap<RestaurantCurrencyType, GetRestaurantCuisinesByRestaurantIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CurrencyTypeId));
    }
}
