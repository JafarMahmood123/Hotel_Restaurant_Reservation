using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;

public class AddReviewCommand : ICommand<Review?>
{
    public AddReviewCommand(Guid restaurantId, Guid customerId, Review review)
    {
        RestaurantId = restaurantId;
        CustomerId = customerId;
        Review = review;
    }

    public Guid RestaurantId { get; }
    public Guid CustomerId { get; }
    public Review Review { get; }
}
