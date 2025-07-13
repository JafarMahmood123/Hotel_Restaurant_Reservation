using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries.GetAllEventReviews;

public class GetAllEventReviewsQueryHandler : IQueryHandler<GetAllEventReviewsQuery, Result<IEnumerable<EventReviewResponse>>>
{
    private readonly IGenericRepository<EventReview> _eventReviewRepository;
    private readonly IMapper _mapper;

    public GetAllEventReviewsQueryHandler(IGenericRepository<EventReview> eventReviewRepository, IMapper mapper)
    {
        _eventReviewRepository = eventReviewRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<EventReviewResponse>>> Handle(GetAllEventReviewsQuery request, CancellationToken cancellationToken)
    {
        var eventReviews = await _eventReviewRepository.GetAllAsync();
        var eventReviewResponses = _mapper.Map<IEnumerable<EventReviewResponse>>(eventReviews);
        return Result.Success(eventReviewResponses);
    }
}