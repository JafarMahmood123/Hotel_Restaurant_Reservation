using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries.GetAllHotelReviewsByUserId
{
    public class GetAllHotelReviewsByUserIdQueryHandler : IQueryHandler<GetAllHotelReviewsByUserIdQuery, Result<IEnumerable<HotelReviewResponse>>>
    {
        private readonly IGenericRepository<HotelReview> _hotelReviewRepository;
        private readonly IMapper _mapper;

        public GetAllHotelReviewsByUserIdQueryHandler(IGenericRepository<HotelReview> hotelReviewRepository, IMapper mapper)
        {
            _hotelReviewRepository = hotelReviewRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<HotelReviewResponse>>> Handle(GetAllHotelReviewsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var hotelReviews = await _hotelReviewRepository
                .Where(hr => hr.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            var hotelReviewResponses = _mapper.Map<IEnumerable<HotelReviewResponse>>(hotelReviews);

            return Result.Success(hotelReviewResponses);
        }
    }
}
