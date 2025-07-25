using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Hotel_Restaurant_Reservation.Application.Abstractions.Payment;
using PayPalCheckoutSdk.Orders;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;

public class AddRestaurantBookingCommandHandler : ICommandHandler<AddRestaurantBookingCommand, Result<RestaurantBookingResponse>>
{
    private readonly IGenericRepository<RestaurantBooking> _restaurantBookingRepository;
    private readonly IGenericRepository<BookingDish> _bookingDishesRepository;
    private readonly IGenericRepository<RestaurantDishPrice> _restaurantDishPriceRepository;
    private readonly IGenericRepository<RestaurantBookingPayment> _restaurantBookingPaymentRepository;
    private readonly IPayPalService _payPalService;
    private readonly IMapper _mapper;

    public AddRestaurantBookingCommandHandler(
        IGenericRepository<RestaurantBooking> restaurantBookingRepository,
        IGenericRepository<BookingDish> bookingDishesRepository,
        IGenericRepository<RestaurantDishPrice> restaurantDishPriceRepository,
        IGenericRepository<RestaurantBookingPayment> restaurantBookingPaymentRepository,
        IPayPalService payPalService,
        IMapper mapper)
    {
        _restaurantBookingRepository = restaurantBookingRepository;
        _bookingDishesRepository = bookingDishesRepository;
        _restaurantDishPriceRepository = restaurantDishPriceRepository;
        _restaurantBookingPaymentRepository = restaurantBookingPaymentRepository;
        _payPalService = payPalService;
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

        var bookingResponse = _mapper.Map<RestaurantBookingResponse>(restaurantBooking);

        return Result.Success(bookingResponse);
    }
}