using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.UpdateEvent;

public class UpdateEventCommandHandler : ICommandHandler<UpdateEventCommand, Result<EventResponse>>
{
    private readonly IGenericRepository<Event> _eventRepository;
    private readonly IMapper _mapper;

    public UpdateEventCommandHandler(IGenericRepository<Event> eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<Result<EventResponse>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var anEvent = await _eventRepository.GetByIdAsync(request.Id);

        if (anEvent is null)
        {
            return Result.Failure<EventResponse>(DomainErrors.Event.NotFound(request.Id));
        }

        _mapper.Map(request.UpdateEventRequest, anEvent);

        await _eventRepository.UpdateAsync(request.Id, anEvent);
        await _eventRepository.SaveChangesAsync();

        var eventResponse = _mapper.Map<EventResponse>(anEvent);

        return Result.Success(eventResponse);
    }
}