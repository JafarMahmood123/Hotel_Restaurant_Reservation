using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantBookingDTOs;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class RestaurantBookingProfile : Profile
{
    public RestaurantBookingProfile()
    {
        CreateMap<RestaurantBooking, RestaurantBookingResponse>();

        CreateMap<AddRestaurantBookingRequest, RestaurantBooking>()
        .ForMember(dest => dest.BookingDishes,
            opt => opt.MapFrom(src => src.AddBookingDishRequest.Select(abd => new BookingDish
            {
                DishId = abd.DishId,
                Quantity = abd.Quantity,
            }).ToList()))
        .AfterMap((src, dest) =>
        {
            // Set the back-reference after mapping
            foreach (var dish in dest.BookingDishes)
            {
                dish.RestaurantBookingId = dest.Id;
            }
        });
    }
}
