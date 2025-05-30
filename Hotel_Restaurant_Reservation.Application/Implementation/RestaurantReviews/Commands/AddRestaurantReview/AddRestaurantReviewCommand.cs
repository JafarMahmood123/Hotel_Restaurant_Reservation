using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;

public class AddRestaurantReviewCommand : ICommand<Result<RestaurantReviewResponse>>
{
    public AddRestaurantReviewCommand(AddRestaurantReviewRequest addRestaurantReviewRequest)
    {
        AddRestaurantReviewRequest = addRestaurantReviewRequest;
    }

    public AddRestaurantReviewRequest AddRestaurantReviewRequest { get; }
}
