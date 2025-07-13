using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries.GetEventById;

public class GetEventByIdQueryHandler : IQueryHandler<GetEventByIdQuery, Result<EventResponse>>
{
    private readonly IGenericRepository<Event> _eventRepository;
    private readonly IMapper _mapper;

    public GetEventByIdQueryHandler(IGenericRepository<Event> eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<Result<EventResponse>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var anEvent = await _eventRepository.GetByIdAsync(request.Id);

        if (anEvent is null)
        {
            return Result.Failure<EventResponse>(DomainErrors.Event.NotFound(request.Id));
        }

        var eventResponse = _mapper.Map<EventResponse>(anEvent);
        return Result.Success(eventResponse);
    }
}