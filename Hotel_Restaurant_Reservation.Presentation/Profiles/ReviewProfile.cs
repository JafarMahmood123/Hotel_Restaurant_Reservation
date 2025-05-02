using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.ReviewDTOs;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<AddReviewRequest, Review>();

        CreateMap<Review, ReviewResponse>();
    }
}
