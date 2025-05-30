using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;

public class AddRestaurantReviewCommandHandler : ICommandHandler<AddRestaurantReviewCommand, Result<RestaurantReviewResponse>>
{
    private readonly IGenericRepository<RestaurantReview> _restaurantReviewRepository;
    private readonly IMapper _mapper;

    public AddRestaurantReviewCommandHandler(IGenericRepository<RestaurantReview> restaurantReviewRepository, IMapper mapper)
    {
        this._restaurantReviewRepository = restaurantReviewRepository;
        this._mapper = mapper;
    }

    public async Task<Result<RestaurantReviewResponse>> Handle(AddRestaurantReviewCommand request, CancellationToken cancellationToken)
    {
        var restaurantReview = _mapper.Map<RestaurantReview>(request.AddRestaurantReviewRequest);

        if (string.IsNullOrWhiteSpace(restaurantReview.Description))
            return Result.Failure<RestaurantReviewResponse>(DomainErrors.RestaurantReview.EmptyDescription);

        if (restaurantReview.CustomerStarRating < 1)
            return Result.Failure<RestaurantReviewResponse>(DomainErrors.RestaurantReview.RatingLessThanOne);

        if (restaurantReview.CustomerStarRating > 5)
            return Result.Failure<RestaurantReviewResponse>(DomainErrors.RestaurantReview.RatingGreaterThanFive);


        restaurantReview.Id = Guid.NewGuid();

        restaurantReview = await _restaurantReviewRepository.AddAsync(restaurantReview);

        var restaurantReviewResponse = _mapper.Map<RestaurantReviewResponse>(restaurantReview);

        return Result.Success(restaurantReviewResponse);
    }
}
