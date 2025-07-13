using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.AddHotelReview
{
    public class AddHotelReviewCommandHandler : ICommandHandler<AddHotelReviewCommand, Result<HotelReviewResponse>>
    {
        private readonly IGenericRepository<HotelReview> _hotelReviewRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public AddHotelReviewCommandHandler(
            IGenericRepository<HotelReview> hotelReviewRepository,
            IHotelRepository hotelRepository,
            IMapper mapper)
        {
            _hotelReviewRepository = hotelReviewRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Result<HotelReviewResponse>> Handle(
            AddHotelReviewCommand request,
            CancellationToken cancellationToken)
        {
            var hotelReview = _mapper.Map<HotelReview>(request.AddHotelReviewRequest);
            hotelReview.Id = Guid.NewGuid();
            hotelReview.ReviewDateTime = DateTime.UtcNow;

            await _hotelReviewRepository.AddAsync(hotelReview);
            await _hotelReviewRepository.SaveChangesAsync();

            var hotel = await _hotelRepository.GetByIdAsync(request.AddHotelReviewRequest.HotelId);
            if (hotel != null)
            {
                var allReviews = await _hotelReviewRepository
                    .Where(r => r.HotelId == request.AddHotelReviewRequest.HotelId)
                    .ToListAsync();

                hotel.StarRate = allReviews.Average(r => r.OverallRating);
                await _hotelRepository.SaveChangesAsync();
            }

            var hotelReviewResponse = _mapper.Map<HotelReviewResponse>(hotelReview);
            return Result.Success(hotelReviewResponse);
        }
    }
}