using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries.GetAllRestaurantReviews
{
    public class GetAllRestaurantReviewsQueryHandler : IQueryHandler<GetAllRestaurantReviewsQuery, Result<IEnumerable<RestaurantReviewResponse>>>
    {
        private readonly IGenericRepository<RestaurantReview> _restaurantReviewRepository;
        private readonly IMapper _mapper;

        public GetAllRestaurantReviewsQueryHandler(IGenericRepository<RestaurantReview> restaurantReviewRepository, IMapper mapper)
        {
            _restaurantReviewRepository = restaurantReviewRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RestaurantReviewResponse>>> Handle(GetAllRestaurantReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _restaurantReviewRepository.GetAllAsync();
            var reviewResponses = _mapper.Map<IEnumerable<RestaurantReviewResponse>>(reviews);
            return Result.Success(reviewResponses);
        }
    }
}
