using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.DeleteHotelReview;

public class DeleteHotelReviewCommand : ICommand<Result>
{
    public DeleteHotelReviewCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}