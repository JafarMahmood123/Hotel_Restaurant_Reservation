using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

public class AddRestaurantReviewCommandHandler : ICommandHandler<AddRestaurantReviewCommand, Result<RestaurantReviewResponse>>
{
    private readonly IGenericRepository<RestaurantReview> _restaurantReviewRepository;
    private readonly IGenericRepository<Restaurant> _restaurantRepository;
    private readonly IMapper _mapper;

    public AddRestaurantReviewCommandHandler(
        IGenericRepository<RestaurantReview> restaurantReviewRepository,
        IGenericRepository<Restaurant> restaurantRepository,
        IMapper mapper)
    {
        _restaurantReviewRepository = restaurantReviewRepository;
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    public async Task<Result<RestaurantReviewResponse>> Handle(
        AddRestaurantReviewCommand request,
        CancellationToken cancellationToken)
    {
        // Validate the review
        if (string.IsNullOrWhiteSpace(request.AddRestaurantReviewRequest.Description))
            return Result.Failure<RestaurantReviewResponse>(DomainErrors.RestaurantReview.EmptyDescription);

        if (request.AddRestaurantReviewRequest.CustomerStarRating < 1)
            return Result.Failure<RestaurantReviewResponse>(DomainErrors.RestaurantReview.RatingLessThanOne);

        if (request.AddRestaurantReviewRequest.CustomerStarRating > 5)
            return Result.Failure<RestaurantReviewResponse>(DomainErrors.RestaurantReview.RatingGreaterThanFive);

        // Map and create the review
        var restaurantReview = _mapper.Map<RestaurantReview>(request.AddRestaurantReviewRequest);
        restaurantReview.Id = Guid.NewGuid();
        restaurantReview.ReviewDateTime = DateTime.UtcNow;

        // Add the review
        restaurantReview = await _restaurantReviewRepository.AddAsync(restaurantReview);
        await _restaurantReviewRepository.SaveChangesAsync();

        // Get all reviews for this restaurant
        var allReviews = await _restaurantReviewRepository
            .Where(r => r.RestaurantId == request.AddRestaurantReviewRequest.RestaurantId)
            .ToListAsync();

        // Calculate new average ratings
        var restaurant = await _restaurantRepository.GetByIdAsync(request.AddRestaurantReviewRequest.RestaurantId);
        if (restaurant != null)
        {
            restaurant.StarRating = allReviews.Average(r => r.CustomerStarRating);

            await _restaurantRepository.SaveChangesAsync();
        }

        // Map to response
        var restaurantReviewResponse = _mapper.Map<RestaurantReviewResponse>(restaurantReview);
        return Result.Success(restaurantReviewResponse);
    }
}