using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.AssignManagerToEvent
{
    public class AssignManagerToEventCommandHandler : ICommandHandler<AssignManagerToEventCommand, Result>
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IGenericRepository<User> _userRepository;

        public AssignManagerToEventCommandHandler(IGenericRepository<Event> eventRepository, IGenericRepository<User> userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(AssignManagerToEventCommand request, CancellationToken cancellationToken)
        {
            var anEvent = await _eventRepository.GetByIdAsync(request.EventId);
            if (anEvent == null)
            {
                return Result.Failure(DomainErrors.Event.NotFound(request.EventId));
            }

            var manager = await _userRepository.GetByIdAsync(request.ManagerId);
            if (manager == null)
            {
                return Result.Failure(DomainErrors.User.NotFound(request.ManagerId));
            }

            anEvent.EventManagerId = request.ManagerId;
            await _eventRepository.UpdateAsync(request.EventId, anEvent);
            await _eventRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}