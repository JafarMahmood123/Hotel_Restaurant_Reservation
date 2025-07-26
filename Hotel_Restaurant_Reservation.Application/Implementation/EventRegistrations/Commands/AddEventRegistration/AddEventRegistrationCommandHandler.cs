using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Hotel_Restaurant_Reservation.Application.Abstractions.Payment;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.AddEventRegistration;

public class AddEventRegistrationCommandHandler : ICommandHandler<AddEventRegistrationCommand, Result<EventRegistrationResponse>>
{
    private readonly IGenericRepository<EventRegistration> _eventRegistrationRepository;
    private readonly IGenericRepository<Event> _eventRepository;
    private readonly IGenericRepository<EventRegistrationPayment> _eventRegistrationPaymentRepository;
    private readonly IPayPalService _payPalService;
    private readonly IMapper _mapper;

    public AddEventRegistrationCommandHandler(
        IGenericRepository<EventRegistration> eventRegistrationRepository,
        IGenericRepository<Event> eventRepository,
        IGenericRepository<EventRegistrationPayment> eventRegistrationPaymentRepository,
        IPayPalService payPalService,
        IMapper mapper)
    {
        _eventRegistrationRepository = eventRegistrationRepository;
        _eventRepository = eventRepository;
        _eventRegistrationPaymentRepository = eventRegistrationPaymentRepository;
        _payPalService = payPalService;
        _mapper = mapper;
    }

    public async Task<Result<EventRegistrationResponse>> Handle(AddEventRegistrationCommand request, CancellationToken cancellationToken)
    {
        var eventRegistration = _mapper.Map<EventRegistration>(request.AddEventRegistrationRequest);
        eventRegistration.Id = Guid.NewGuid();
        eventRegistration.RegistrationDateTime = DateTime.UtcNow;

        var anEvent = await _eventRepository.GetByIdAsync(request.AddEventRegistrationRequest.EventId);
        if (anEvent == null)
        {
            return Result.Failure<EventRegistrationResponse>(DomainErrors.Event.NotFound(request.AddEventRegistrationRequest.EventId));
        }

        var totalAmount = (decimal)anEvent.PayToEnter * eventRegistration.NumberOfPeople;

        // Create PayPal order
        var payPalOrder = await _payPalService.CreateOrder(totalAmount.ToString("F2"), "USD");

        var createdEventRegistration = await _eventRegistrationRepository.AddAsync(eventRegistration);
        await _eventRegistrationRepository.SaveChangesAsync();

        // Create and save payment details
        var eventRegistrationPayment = new EventRegistrationPayment
        {
            Id = Guid.NewGuid(),
            EventRegistrationId = createdEventRegistration.Id,
            Amount = totalAmount,
            Currency = "USD",
            OrderId = payPalOrder.Id,
            //Status = payPalOrder.Status
        };
        await _eventRegistrationPaymentRepository.AddAsync(eventRegistrationPayment);
        await _eventRegistrationPaymentRepository.SaveChangesAsync();


        var eventRegistrationResponse = _mapper.Map<EventRegistrationResponse>(createdEventRegistration);
        eventRegistrationResponse.PayPalOrderId = payPalOrder.Id;
        eventRegistrationResponse.PayPalOrderStatus = payPalOrder.Status;

        return Result.Success(eventRegistrationResponse);
    }
}