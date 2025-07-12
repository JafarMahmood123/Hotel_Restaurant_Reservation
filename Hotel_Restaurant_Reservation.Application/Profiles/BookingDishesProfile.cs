using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class BookingDishesProfile : Profile
{
    public BookingDishesProfile()
    {
        CreateMap<BookingDish, BookingDishResponse>();
    }
}
