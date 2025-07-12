using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.UpdateEventRegistration;

public class UpdateEventRegistrationCommandHandler : ICommandHandler<UpdateEventRegistrationCommand, Result<EventRegistrationResponse>>
{
    private readonly IGenericRepository<EventRegistration> _eventRegistrationRepository;
    private readonly IMapper _mapper;

    public UpdateEventRegistrationCommandHandler(IGenericRepository<EventRegistration> eventRegistrationRepository, IMapper mapper)
    {
        _eventRegistrationRepository = eventRegistrationRepository;
        _mapper = mapper;
    }

    public async Task<Result<EventRegistrationResponse>> Handle(UpdateEventRegistrationCommand request, CancellationToken cancellationToken)
    {
        var eventRegistration = await _eventRegistrationRepository.GetByIdAsync(request.Id);

        if (eventRegistration is null)
        {
            return Result.Failure<EventRegistrationResponse>(DomainErrors.EventRegistration.NotFound(request.Id));
        }

        _mapper.Map(request.UpdateEventRegistrationRequest, eventRegistration);

        await _eventRegistrationRepository.UpdateAsync(request.Id, eventRegistration);
        await _eventRegistrationRepository.SaveChangesAsync();

        var eventRegistrationResponse = _mapper.Map<EventRegistrationResponse>(eventRegistration);

        return Result.Success(eventRegistrationResponse);
    }
}