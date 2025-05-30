using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.BookingDishDTOs;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class BookingDishesProfile : Profile
{
    public BookingDishesProfile()
    {
        CreateMap<BookingDish, BookingDishResponse>();
    }
}
