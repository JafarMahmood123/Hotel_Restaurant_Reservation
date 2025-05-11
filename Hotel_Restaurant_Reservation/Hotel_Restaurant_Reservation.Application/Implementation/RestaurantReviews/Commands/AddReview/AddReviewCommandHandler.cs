using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;

public class AddReviewCommandHandler : ICommandHandler<AddReviewCommand, Review?>
{
    private readonly IGenericRepository<Review> reviewRepository;
    private readonly IGenericRepository<RestaurantReview> restaurantReviewRepository;

    public AddReviewCommandHandler(IGenericRepository<Review> reviewRepository,
        IGenericRepository<RestaurantReview> restaurantReviewRepository)
    {
        this.reviewRepository = reviewRepository;
        this.restaurantReviewRepository = restaurantReviewRepository;
    }

    public async Task<Review?> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var review = request.Review;
        review.Id = Guid.NewGuid();

        review = await reviewRepository.AddAsync(review);
        await reviewRepository.SaveChangesAsync();

        var customerId = request.CustomerId;
        var restaurantId = request.RestaurantId;

        var restaurantReview = new RestaurantReview()
        {
            Id = Guid.NewGuid(),
            RestaurantId = restaurantId,
            ReviewId = review.Id,
            CustomerId = customerId,
        };

        await restaurantReviewRepository.AddAsync(restaurantReview);
        await restaurantReviewRepository.SaveChangesAsync();

        return review;
    }
}
