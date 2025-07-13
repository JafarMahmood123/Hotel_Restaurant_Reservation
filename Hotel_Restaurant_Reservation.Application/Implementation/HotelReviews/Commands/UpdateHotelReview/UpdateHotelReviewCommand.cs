using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.UpdateHotelReview;

public class UpdateHotelReviewCommand : ICommand<Result<HotelReviewResponse>>
{
    public UpdateHotelReviewCommand(Guid id, UpdateHotelReviewRequest updateHotelReviewRequest)
    {
        Id = id;
        UpdateHotelReviewRequest = updateHotelReviewRequest;
    }

    public Guid Id { get; }
    public UpdateHotelReviewRequest UpdateHotelReviewRequest { get; }
}