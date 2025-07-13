using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries.GetHotelReviewById;

public class GetHotelReviewByIdQueryHandler : IQueryHandler<GetHotelReviewByIdQuery, Result<HotelReviewResponse>>
{
    private readonly IGenericRepository<HotelReview> _hotelReviewRepository;
    private readonly IMapper _mapper;

    public GetHotelReviewByIdQueryHandler(IGenericRepository<HotelReview> hotelReviewRepository, IMapper mapper)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _mapper = mapper;
    }

    public async Task<Result<HotelReviewResponse>> Handle(GetHotelReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var hotelReview = await _hotelReviewRepository.GetByIdAsync(request.Id);

        if (hotelReview is null)
        {
            return Result.Failure<HotelReviewResponse>(DomainErrors.HotelReview.NotFound(request.Id));
        }

        var hotelReviewResponse = _mapper.Map<HotelReviewResponse>(hotelReview);
        return Result.Success(hotelReviewResponse);
    }
}