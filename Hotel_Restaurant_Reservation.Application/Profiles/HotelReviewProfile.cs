using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.AddHotelReview;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.UpdateHotelReview;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles
{
    public class HotelReviewProfile : Profile
    {
        public HotelReviewProfile()
        {
            CreateMap<AddHotelReviewRequest, HotelReview>();
            CreateMap<HotelReview, HotelReviewResponse>();
            CreateMap<UpdateHotelReviewRequest, HotelReview>();
        }
    }
}