using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Hotel_Restaurant_Reservation.Application.Abstractions.Payment;
using Hotel_Restaurant_Reservation.Domain.Enums;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking
{
    public class AddRestaurantBookingCommandHandler : ICommandHandler<AddRestaurantBookingCommand, Result<RestaurantBookingResponse>>
    {
        private readonly IGenericRepository<RestaurantBooking> _restaurantBookingRepository;
        private readonly IGenericRepository<BookingDish> _bookingDishesRepository;
        private readonly IGenericRepository<RestaurantDishPrice> _restaurantDishPriceRepository;
        private readonly IGenericRepository<RestaurantBookingPayment> _restaurantBookingPaymentRepository;
        private readonly IGenericRepository<CurrencyType> _currencyTypeRepository;
        private readonly ILocalPaymentService _localPaymentService;
        private readonly IMapper _mapper;

        public AddRestaurantBookingCommandHandler(
            IGenericRepository<RestaurantBooking> restaurantBookingRepository,
            IGenericRepository<BookingDish> bookingDishesRepository,
            IGenericRepository<RestaurantDishPrice> restaurantDishPriceRepository,
            IGenericRepository<RestaurantBookingPayment> restaurantBookingPaymentRepository,
            IGenericRepository<CurrencyType> currencyTypeRepository,
            ILocalPaymentService localPaymentService,
            IMapper mapper)
        {
            _restaurantBookingRepository = restaurantBookingRepository;
            _bookingDishesRepository = bookingDishesRepository;
            _restaurantDishPriceRepository = restaurantDishPriceRepository;
            _restaurantBookingPaymentRepository = restaurantBookingPaymentRepository;
            _currencyTypeRepository = currencyTypeRepository;
            _localPaymentService = localPaymentService;
            _mapper = mapper;
        }

        public async Task<Result<RestaurantBookingResponse>> Handle(AddRestaurantBookingCommand request,
            CancellationToken cancellationToken)
        {
            var restaurantBooking = _mapper.Map<RestaurantBooking>(request.AddRestaurantBookingRequest);

            var existingBookingAtTheRecieveTime = await _restaurantBookingRepository.GetFirstOrDefaultAsync(
                x => x.TableNumber == restaurantBooking.TableNumber &&
                (restaurantBooking.ReceiveDateTime >= x.ReceiveDateTime &&
                restaurantBooking.ReceiveDateTime <= (x.ReceiveDateTime.AddMinutes(x.BookingDurationTime.AddMinutes(10).Minute))));


            if (existingBookingAtTheRecieveTime != null)
                return Result.Failure<RestaurantBookingResponse>(DomainErrors.RestaurantBooking.BookedTableAtThisTime(
                    restaurantBooking.TableNumber, restaurantBooking.ReceiveDateTime));

            restaurantBooking.Id = Guid.NewGuid();
            restaurantBooking.BookingDateTime = DateTime.Now;
            restaurantBooking.UserId = request.AddRestaurantBookingRequest.UserId;

            var currencyType = await _currencyTypeRepository.GetByIdAsync(request.AddRestaurantBookingRequest.CurrencyTypeId);
            if (currencyType == null)
            {
                return Result.Failure<RestaurantBookingResponse>(DomainErrors.CurrencyType.NotFound(request.AddRestaurantBookingRequest.CurrencyTypeId));
            }

            if ((restaurantBooking.BookingDurationTime.Minute + restaurantBooking.BookingDurationTime.Hour * 60) < 15)
                return Result.Failure<RestaurantBookingResponse>(DomainErrors.RestaurantBooking.ShortBookingTime());


            if ((restaurantBooking.BookingDurationTime.Minute + restaurantBooking.BookingDurationTime.Hour * 60) > 60)
                return Result.Failure<RestaurantBookingResponse>(DomainErrors.RestaurantBooking.LongBookingTime());

            restaurantBooking = await _restaurantBookingRepository.AddAsync(restaurantBooking);
            await _restaurantBookingRepository.SaveChangesAsync();


            var bookingDishes = request.AddRestaurantBookingRequest.AddBookingDishRequest.dishesIdsWithQuantities;
            decimal totalAmount = 0;

            foreach (var bookingDishId in bookingDishes.Keys)
            {
                var dishPrice = await _restaurantDishPriceRepository.GetFirstOrDefaultAsync(dp => dp.DishId == bookingDishId && dp.RestaurantId == restaurantBooking.RestaurantId);
                if (dishPrice != null)
                {
                    totalAmount += (decimal)dishPrice.Price * bookingDishes[bookingDishId];
                }

                var bookingDish = new BookingDish()
                {
                    Id = Guid.NewGuid(),
                    RestaurantBookingId = restaurantBooking.Id,
                    DishId = bookingDishId,
                    Quantity = bookingDishes[bookingDishId],
                };

                await _bookingDishesRepository.AddAsync(bookingDish);
                await _bookingDishesRepository.SaveChangesAsync();

                restaurantBooking.BookingDishes.Add(bookingDish);
            }

            var orderId = await _localPaymentService.CreateOrder(totalAmount, currencyType.CurrencyCode);
            var captured = await _localPaymentService.CaptureOrder(orderId);

            if (captured)
            {
                var restaurantBookingPayment = new RestaurantBookingPayment
                {
                    Id = Guid.NewGuid(),
                    RestaurantBookingId = restaurantBooking.Id,
                    Amount = totalAmount,
                    Currency = currencyType.CurrencyCode,
                    OrderId = orderId,
                    Status = PaymentStatus.Pending,
                };
                await _restaurantBookingPaymentRepository.AddAsync(restaurantBookingPayment);
                await _restaurantBookingPaymentRepository.SaveChangesAsync();
            }

            var bookingResponse = _mapper.Map<RestaurantBookingResponse>(restaurantBooking);

            return Result.Success(bookingResponse);
        }
    }
}
