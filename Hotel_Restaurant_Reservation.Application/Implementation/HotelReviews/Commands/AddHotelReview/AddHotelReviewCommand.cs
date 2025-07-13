using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.AddHotelReview
{
    public class AddHotelReviewCommand : ICommand<Result<HotelReviewResponse>>
    {
        public AddHotelReviewRequest AddHotelReviewRequest { get; }

        public AddHotelReviewCommand(AddHotelReviewRequest addHotelReviewRequest)
        {
            AddHotelReviewRequest = addHotelReviewRequest;
        }
    }
}