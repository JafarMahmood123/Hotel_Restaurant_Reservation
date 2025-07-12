using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.AddEvent;

public class AddEventCommandHandler : ICommandHandler<AddEventCommand, Result<EventResponse>>
{
    private readonly IGenericRepository<Event> _eventRepository;
    private readonly IMapper _mapper;

    public AddEventCommandHandler(IGenericRepository<Event> eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<Result<EventResponse>> Handle(AddEventCommand request, CancellationToken cancellationToken)
    {
        var anEvent = _mapper.Map<Event>(request.AddEventRequest);
        anEvent.Id = Guid.NewGuid();

        var createdEvent = await _eventRepository.AddAsync(anEvent);
        await _eventRepository.SaveChangesAsync();

        var eventResponse = _mapper.Map<EventResponse>(createdEvent);

        return Result.Success(eventResponse);
    }
}