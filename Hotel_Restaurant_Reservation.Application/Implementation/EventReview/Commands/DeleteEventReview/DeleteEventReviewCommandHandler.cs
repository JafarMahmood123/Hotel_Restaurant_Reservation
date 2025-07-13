using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.DeleteEventReview;

public class DeleteEventReviewCommandHandler : ICommandHandler<DeleteEventReviewCommand, Result>
{
    private readonly IGenericRepository<EventReview> _eventReviewRepository;

    public DeleteEventReviewCommandHandler(IGenericRepository<EventReview> eventReviewRepository)
    {
        _eventReviewRepository = eventReviewRepository;
    }

    public async Task<Result> Handle(DeleteEventReviewCommand request, CancellationToken cancellationToken)
    {
        var eventReview = await _eventReviewRepository.GetByIdAsync(request.Id);

        if (eventReview is null)
        {
            return Result.Failure(DomainErrors.EventReview.NotFound(request.Id));
        }

        await _eventReviewRepository.RemoveAsync(request.Id);
        await _eventReviewRepository.SaveChangesAsync();

        return Result.Success();
    }
}