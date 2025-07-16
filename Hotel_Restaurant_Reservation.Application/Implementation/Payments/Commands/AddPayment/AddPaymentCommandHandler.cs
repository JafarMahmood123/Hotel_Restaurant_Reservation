using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Payments.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Payments.Commands.AddPayment;

public class AddPaymentCommandHandler : ICommandHandler<AddPaymentCommand, Result<PaymentResponse>>
{
    private readonly IGenericRepository<Payment> _paymentRepository;
    private readonly IMapper _mapper;

    public AddPaymentCommandHandler(IGenericRepository<Payment> paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaymentResponse>> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = _mapper.Map<Payment>(request.AddPaymentRequest);
        payment.Id = Guid.NewGuid();
        payment.Status = "Completed"; // Assuming payment is completed upon creation

        await _paymentRepository.AddAsync(payment);
        await _paymentRepository.SaveChangesAsync();

        var paymentResponse = _mapper.Map<PaymentResponse>(payment);

        return Result.Success(paymentResponse);
    }
}