using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries.GetAllEventRegistration;

public class GetAllEventRegistrationsQueryHandler : IQueryHandler<GetAllEventRegistrationsQuery, Result<IEnumerable<EventRegistrationResponse>>>
{
    private readonly IGenericRepository<EventRegistration> _eventRegistrationRepository;
    private readonly IMapper _mapper;

    public GetAllEventRegistrationsQueryHandler(IGenericRepository<EventRegistration> eventRegistrationRepository, IMapper mapper)
    {
        _eventRegistrationRepository = eventRegistrationRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<EventRegistrationResponse>>> Handle(GetAllEventRegistrationsQuery request, CancellationToken cancellationToken)
    {
        var eventRegistrations = await _eventRegistrationRepository.GetAllAsync();
        var eventRegistrationResponses = _mapper.Map<IEnumerable<EventRegistrationResponse>>(eventRegistrations);
        return Result.Success(eventRegistrationResponses);
    }
}