using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.AddEventReview;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles
{
    public class EventReviewProfile : Profile
    {
        public EventReviewProfile()
        {
            CreateMap<AddEventReviewRequest, EventReview>();
            CreateMap<EventReview, EventReviewResponse>();
        }
    }
}