using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.DeleteEvent;

public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommand, Result>
{
    private readonly IGenericRepository<Event> _eventRepository;

    public DeleteEventCommandHandler(IGenericRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var anEvent = await _eventRepository.GetByIdAsync(request.Id);

        if (anEvent is null)
        {
            return Result.Failure(DomainErrors.Event.NotFound(request.Id));
        }

        await _eventRepository.RemoveAsync(request.Id);
        await _eventRepository.SaveChangesAsync();

        return Result.Success();
    }
}