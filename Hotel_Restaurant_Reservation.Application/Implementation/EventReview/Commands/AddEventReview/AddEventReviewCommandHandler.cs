using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.AddEventReview
{
    public class AddEventReviewCommandHandler : ICommandHandler<AddEventReviewCommand, Result<EventReviewResponse>>
    {
        private readonly IGenericRepository<EventReview> _eventReviewRepository;
        private readonly IMapper _mapper;

        public AddEventReviewCommandHandler(
            IGenericRepository<EventReview> eventReviewRepository,
            IMapper mapper)
        {
            _eventReviewRepository = eventReviewRepository;
            _mapper = mapper;
        }

        public async Task<Result<EventReviewResponse>> Handle(
            AddEventReviewCommand request,
            CancellationToken cancellationToken)
        {
            var eventReview = _mapper.Map<EventReview>(request.AddEventReviewRequest);
            eventReview.Id = Guid.NewGuid();
            eventReview.ReviewDateTime = DateTime.UtcNow;

            await _eventReviewRepository.AddAsync(eventReview);
            await _eventReviewRepository.SaveChangesAsync();

            var eventReviewResponse = _mapper.Map<EventReviewResponse>(eventReview);
            return Result.Success(eventReviewResponse);
        }
    }
}