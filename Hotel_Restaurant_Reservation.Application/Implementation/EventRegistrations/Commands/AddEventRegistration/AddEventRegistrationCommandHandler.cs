using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.AddEventRegistration;

public class AddEventRegistrationCommandHandler : ICommandHandler<AddEventRegistrationCommand, Result<EventRegistrationResponse>>
{
    private readonly IGenericRepository<EventRegistration> _eventRegistrationRepository;
    private readonly IMapper _mapper;

    public AddEventRegistrationCommandHandler(IGenericRepository<EventRegistration> eventRegistrationRepository, IMapper mapper)
    {
        _eventRegistrationRepository = eventRegistrationRepository;
        _mapper = mapper;
    }

    public async Task<Result<EventRegistrationResponse>> Handle(AddEventRegistrationCommand request, CancellationToken cancellationToken)
    {
        var eventRegistration = _mapper.Map<EventRegistration>(request.AddEventRegistrationRequest);
        eventRegistration.Id = Guid.NewGuid();
        eventRegistration.RegistrationDateTime = DateTime.UtcNow;

        var createdEventRegistration = await _eventRegistrationRepository.AddAsync(eventRegistration);
        await _eventRegistrationRepository.SaveChangesAsync();

        var eventRegistrationResponse = _mapper.Map<EventRegistrationResponse>(createdEventRegistration);

        return Result.Success(eventRegistrationResponse);
    }
}