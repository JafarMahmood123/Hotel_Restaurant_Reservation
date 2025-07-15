using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries.GetAllRestaurantReviewsByRestaurantId
{
    public class GetAllRestaurantReviewsByRestaurantIdQueryHandler : IQueryHandler<GetAllRestaurantReviewsByRestaurantIdQuery, Result<IEnumerable<RestaurantReviewResponse>>>
    {
        private readonly IGenericRepository<RestaurantReview> _restaurantReviewRepository;
        private readonly IMapper _mapper;

        public GetAllRestaurantReviewsByRestaurantIdQueryHandler(IGenericRepository<RestaurantReview> restaurantReviewRepository, IMapper mapper)
        {
            _restaurantReviewRepository = restaurantReviewRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RestaurantReviewResponse>>> Handle(GetAllRestaurantReviewsByRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            var restaurantReviews = await _restaurantReviewRepository
                .Where(rr => rr.RestaurantId == request.RestaurantId)
                .ToListAsync(cancellationToken);

            var reviewResponses = _mapper.Map<IEnumerable<RestaurantReviewResponse>>(restaurantReviews);

            return Result.Success(reviewResponses);
        }
    }
}
