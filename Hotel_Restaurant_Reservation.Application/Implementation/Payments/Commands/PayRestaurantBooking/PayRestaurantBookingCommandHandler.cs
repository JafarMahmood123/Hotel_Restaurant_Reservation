using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Payments.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Payments.Commands.PayRestaurantBooking;

public class PayRestaurantBookingCommandHandler : ICommandHandler<PayRestaurantBookingCommand, Result<RestaurantBookingPaymentResponse>>
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<RestaurantBooking> _bookingRepository;
    private readonly IGenericRepository<RestaurantBookingPayment> _paymentRepository;
    private readonly IGenericRepository<CurrencyType> _currencyTypeRepository;
    private readonly IMapper _mapper;

    public PayRestaurantBookingCommandHandler(
        IGenericRepository<User> userRepository,
        IGenericRepository<RestaurantBooking> bookingRepository,
        IGenericRepository<RestaurantBookingPayment> paymentRepository,
        IGenericRepository<CurrencyType> currencyTypeRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _bookingRepository = bookingRepository;
        _paymentRepository = paymentRepository;
        _currencyTypeRepository = currencyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<RestaurantBookingPaymentResponse>> Handle(PayRestaurantBookingCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.Request.UserId);
        if (user == null)
        {
            return Result.Failure< RestaurantBookingPaymentResponse>
                (DomainErrors.User.NotFound(command.Request.UserId));
        }

        var booking = await _bookingRepository.GetByIdAsync(command.Request.RestaurantBookingId);
        if (booking == null)
        {
            return Result.Failure< RestaurantBookingPaymentResponse>
                (DomainErrors.RestaurantBooking.NotFound(command.Request.RestaurantBookingId));
        }

        var currencyType = await _currencyTypeRepository.GetByIdAsync(command.Request.CurrencyTypeId);
        if (currencyType == null)
        {
            return Result.Failure<RestaurantBookingPaymentResponse>
                (DomainErrors.CurrencyType.NotFound(command.Request.CurrencyTypeId));
        }

        var payment = await _paymentRepository.GetByIdAsync(command.Request.RestaurantBookingId);
        if (payment == null)
        {
            return Result.Failure<RestaurantBookingPaymentResponse>
                (DomainErrors.Payment.NotFound(command.Request.CurrencyTypeId));
        }

        if (user.Balance < payment.Amount)
        {
            return Result.Failure<RestaurantBookingPaymentResponse>
                (DomainErrors.Payment.InsufficientFunds());
        }

        user.Balance -= payment.Amount;

        payment.CurrencyTypeId = command.Request.CurrencyTypeId;
        payment.Status = PaymentStatus.Paid;

        await _paymentRepository.UpdateAsync(payment.Id, payment);
        await _userRepository.SaveChangesAsync();


        var response = _mapper.Map<RestaurantBookingPaymentResponse>(payment);
        return Result.Success(response);
    }
}