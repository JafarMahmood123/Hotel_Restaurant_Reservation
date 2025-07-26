using System.Threading;
using System.Threading.Tasks;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Payment;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Payments.PayRestaurantBooking
{
    public class PayRestaurantBookingCommandHandler : ICommandHandler<PayRestaurantBookingCommand, Result>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<RestaurantBooking> _bookingRepository;
        private readonly IGenericRepository<RestaurantBookingPayment> _paymentRepository;
        private readonly IGenericRepository<CurrencyType> _currencyTypeRepository;
        private readonly ILocalPaymentService _localPaymentService;

        public PayRestaurantBookingCommandHandler(
            IGenericRepository<User> userRepository,
            IGenericRepository<RestaurantBooking> bookingRepository,
            IGenericRepository<RestaurantBookingPayment> paymentRepository,
            IGenericRepository<CurrencyType> currencyTypeRepository,
            ILocalPaymentService localPaymentService)
        {
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _paymentRepository = paymentRepository;
            _currencyTypeRepository = currencyTypeRepository;
            _localPaymentService = localPaymentService;
        }

        public async Task<Result> Handle(PayRestaurantBookingCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.Request.UserId);
            if (user == null)
            {
                return Result.Failure(DomainErrors.User.NotFound(command.Request.UserId));
            }

            var booking = await _bookingRepository.GetByIdAsync(command.Request.RestaurantBookingId);
            if (booking == null)
            {
                return Result.Failure(DomainErrors.RestaurantBooking.NotFound(command.Request.RestaurantBookingId));
            }

            var currencyType = await _currencyTypeRepository.GetByIdAsync(command.Request.CurrencyTypeId);
            if (currencyType == null)
            {
                return Result.Failure<Result>(DomainErrors.CurrencyType.NotFound(command.Request.CurrencyTypeId));
            }

            if (user.Balance < command.Request.Amount)
            {
                return Result.Failure(DomainErrors.Payment.InsufficientFunds());
            }

            user.Balance -= command.Request.Amount;

            var orderId = await _localPaymentService.CreateOrder(command.Request.Amount, currencyType.CurrencyCode);

            var payment = new RestaurantBookingPayment
            {
                Id = Guid.NewGuid(),
                RestaurantBookingId = booking.Id,
                Amount = command.Request.Amount,
                Currency = currencyType.CurrencyCode,
                Status = PaymentStatus.Paid,
                OrderId = orderId
            };

            await _paymentRepository.AddAsync(payment);
            await _userRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}