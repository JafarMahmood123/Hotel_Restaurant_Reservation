using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries.GetAllEventRegistrationsByEventId;

public class GetAllEventRegistrationsByEventIdQueryHandler : IQueryHandler<GetAllEventRegistrationsByEventIdQuery, Result<IEnumerable<EventRegistrationResponse>>>
{
    private readonly IGenericRepository<EventRegistration> _eventRegistrationRepository;
    private readonly IMapper _mapper;

    public GetAllEventRegistrationsByEventIdQueryHandler(IGenericRepository<EventRegistration> eventRegistrationRepository, IMapper mapper)
    {
        _eventRegistrationRepository = eventRegistrationRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<EventRegistrationResponse>>> Handle(GetAllEventRegistrationsByEventIdQuery request, CancellationToken cancellationToken)
    {
        var eventRegistrations = await _eventRegistrationRepository.Where(x => x.EventId == request.EventId).ToListAsync(cancellationToken: cancellationToken);
        var eventRegistrationResponses = _mapper.Map<IEnumerable<EventRegistrationResponse>>(eventRegistrations);
        return Result.Success(eventRegistrationResponses);
    }
}