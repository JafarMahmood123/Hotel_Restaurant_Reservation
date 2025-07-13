using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries.GetEventReviewById;

public class GetEventReviewByIdQueryHandler : IQueryHandler<GetEventReviewByIdQuery, Result<EventReviewResponse>>
{
    private readonly IGenericRepository<EventReview> _eventReviewRepository;
    private readonly IMapper _mapper;

    public GetEventReviewByIdQueryHandler(IGenericRepository<EventReview> eventReviewRepository, IMapper mapper)
    {
        _eventReviewRepository = eventReviewRepository;
        _mapper = mapper;
    }

    public async Task<Result<EventReviewResponse>> Handle(GetEventReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var eventReview = await _eventReviewRepository.GetByIdAsync(request.Id);

        if (eventReview is null)
        {
            return Result.Failure<EventReviewResponse>(DomainErrors.EventReview.NotFound(request.Id));
        }

        var eventReviewResponse = _mapper.Map<EventReviewResponse>(eventReview);
        return Result.Success(eventReviewResponse);
    }
}