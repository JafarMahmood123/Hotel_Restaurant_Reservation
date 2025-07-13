// Hotel_Restaurant_Reservation.Application/Implementation/EventReviews/Commands/UpdateEventReview/UpdateEventReviewCommandHandler.cs
using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.UpdateEventReview;

public class UpdateEventReviewCommandHandler : ICommandHandler<UpdateEventReviewCommand, Result<EventReviewResponse>>
{
    private readonly IGenericRepository<EventReview> _eventReviewRepository;
    private readonly IMapper _mapper;

    public UpdateEventReviewCommandHandler(IGenericRepository<EventReview> eventReviewRepository, IMapper mapper)
    {
        _eventReviewRepository = eventReviewRepository;
        _mapper = mapper;
    }

    public async Task<Result<EventReviewResponse>> Handle(UpdateEventReviewCommand request, CancellationToken cancellationToken)
    {
        var eventReview = await _eventReviewRepository.GetByIdAsync(request.Id);

        if (eventReview is null)
        {
            return Result.Failure<EventReviewResponse>(DomainErrors.EventReview.NotFound(request.Id));
        }

        _mapper.Map(request.UpdateEventReviewRequest, eventReview);

        await _eventReviewRepository.UpdateAsync(request.Id, eventReview);
        await _eventReviewRepository.SaveChangesAsync();

        var eventReviewResponse = _mapper.Map<EventReviewResponse>(eventReview);

        return Result.Success(eventReviewResponse);
    }
}