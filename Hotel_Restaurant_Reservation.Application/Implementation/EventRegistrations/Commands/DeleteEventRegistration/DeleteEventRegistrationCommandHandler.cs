using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.DeleteEventRegistration;

public class DeleteEventRegistrationCommandHandler : ICommandHandler<DeleteEventRegistrationCommand, Result>
{
    private readonly IGenericRepository<EventRegistration> _eventRegistrationRepository;

    public DeleteEventRegistrationCommandHandler(IGenericRepository<EventRegistration> eventRegistrationRepository)
    {
        _eventRegistrationRepository = eventRegistrationRepository;
    }

    public async Task<Result> Handle(DeleteEventRegistrationCommand request, CancellationToken cancellationToken)
    {
        var eventRegistration = await _eventRegistrationRepository.GetByIdAsync(request.Id);

        if (eventRegistration is null)
        {
            return Result.Failure(DomainErrors.EventRegistration.NotFound(request.Id));
        }

        await _eventRegistrationRepository.RemoveAsync(request.Id);
        await _eventRegistrationRepository.SaveChangesAsync();

        return Result.Success();
    }
}