using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantBookingDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Errors;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;

public class AddRestaurantBookingCommandHandler : ICommandHandler<AddRestaurantBookingCommand, Result<RestaurantBookingResponse>>
{
    private readonly IGenericRepository<RestaurantBooking> _restaurantBookingRepository;
    private readonly IGenericRepository<BookingDish> _bookingDishesRepository;
    private readonly IMapper _mapper;

    public AddRestaurantBookingCommandHandler(IGenericRepository<RestaurantBooking> restaurantBookingRepository,
        IGenericRepository<BookingDish> bookingDishesRepository, IMapper mapper)
    {
        this._restaurantBookingRepository = restaurantBookingRepository;
        this._bookingDishesRepository = bookingDishesRepository;
        this._mapper = mapper;
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
            return Result.Failure<RestaurantBookingResponse>(DomainErrors.RestaurantBooking.BookedTableAtThisTime);

        restaurantBooking.Id = Guid.NewGuid();
        restaurantBooking.BookingDateTime = DateTime.Now;

        if (restaurantBooking.BookingDurationTime.Minute < 15)
            return Result.Failure<RestaurantBookingResponse>(DomainErrors.RestaurantBooking.ShortBookingTime);


        if (restaurantBooking.BookingDurationTime.Minute > 60)
            return Result.Failure<RestaurantBookingResponse>(DomainErrors.RestaurantBooking.LongBookingTime);


        restaurantBooking = await _restaurantBookingRepository.AddAsync(restaurantBooking);
        await _restaurantBookingRepository.SaveChangesAsync();


        var bookingDishes = request.AddRestaurantBookingRequest.AddBookingDishRequest.dishesIdsWithQuantities;
        foreach (var bookingDishId in bookingDishes.Keys)
        {
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
