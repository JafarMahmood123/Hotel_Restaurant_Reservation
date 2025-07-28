using Application.Features.Tags.Queries.GetTagsByRestaurantId;
using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetFeaturesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetMealTypesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantCuisinesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetWorkTimesByRestaurantId;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RestaurantProfile : Profile
{

    public RestaurantProfile()
    {

        CreateMap<Restaurant, RestaurantResponse>();

        CreateMap<AddRestaurantRequest, Restaurant>();

        CreateMap<RestaurantUpdateRequest, Restaurant>();

        CreateMap<RestaurantCuisine, GetRestaurantCuisinesByRestaurantIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Cuisine.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Cuisine.Name));

        CreateMap<RestaurantMealType, GetMealTypesByRestaurantIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MealTypeId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MealType.Name));

        CreateMap<RestaurantTag, GetTagsByRestaurantIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TagId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Tag.Name));

        CreateMap<RestaurantFeature, GetFeaturesByRestaurantIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FeatureId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Feature.Name));

        CreateMap<RestaurantWorkTime, GetWorkTimesByRestaurantIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkTimeId));
    }
}
