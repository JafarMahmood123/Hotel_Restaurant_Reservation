using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class RestaurantReviewProfile : Profile
{
    public RestaurantReviewProfile()
    {
        CreateMap<AddRestaurantReviewRequest, RestaurantReview>();

        CreateMap<RestaurantReview, RestaurantReviewResponse>();
    }
}
