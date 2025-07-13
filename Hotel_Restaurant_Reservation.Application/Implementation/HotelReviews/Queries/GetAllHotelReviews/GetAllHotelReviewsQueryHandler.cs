using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries.GetAllHotelReviews;

public class GetAllHotelReviewsQueryHandler : IQueryHandler<GetAllHotelReviewsQuery, Result<IEnumerable<HotelReviewResponse>>>
{
    private readonly IGenericRepository<HotelReview> _hotelReviewRepository;
    private readonly IMapper _mapper;

    public GetAllHotelReviewsQueryHandler(IGenericRepository<HotelReview> hotelReviewRepository, IMapper mapper)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<HotelReviewResponse>>> Handle(GetAllHotelReviewsQuery request, CancellationToken cancellationToken)
    {
        var hotelReviews = await _hotelReviewRepository.GetAllAsync();
        var hotelReviewResponses = _mapper.Map<IEnumerable<HotelReviewResponse>>(hotelReviews);
        return Result.Success(hotelReviewResponses);
    }
}