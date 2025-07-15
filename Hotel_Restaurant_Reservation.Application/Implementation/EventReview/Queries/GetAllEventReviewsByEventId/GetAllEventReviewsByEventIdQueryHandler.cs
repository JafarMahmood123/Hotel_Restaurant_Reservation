using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries.GetAllEventReviewsByEventId;

public class GetAllEventReviewsByEventIdQueryHandler : IQueryHandler<GetAllEventReviewsByEventIdQuery, Result<IEnumerable<EventReviewResponse>>>
{
    private readonly IGenericRepository<EventReview> _eventReviewRepository;
    private readonly IMapper _mapper;

    public GetAllEventReviewsByEventIdQueryHandler(IGenericRepository<EventReview> eventReviewRepository, IMapper mapper)
    {
        _eventReviewRepository = eventReviewRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<EventReviewResponse>>> Handle(GetAllEventReviewsByEventIdQuery request, CancellationToken cancellationToken)
    {
        var eventReviews = await _eventReviewRepository
            .Where(er => er.EventId == request.EventId)
            .ToListAsync(cancellationToken);

        var eventReviewResponses = _mapper.Map<IEnumerable<EventReviewResponse>>(eventReviews);

        return Result.Success(eventReviewResponses);
    }
}