using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.UpdateBookingDishes;

public class UpdateBookingDishesCommandHandler : ICommandHandler<UpdateBookingDishesCommand, Result>
{
    private readonly IGenericRepository<BookingDish> _bookingDishRepository;
    private readonly IGenericRepository<RestaurantBooking> _restaurantBookingRepository;
    private readonly IGenericRepository<Dish> _dishRepository;

    public UpdateBookingDishesCommandHandler(
        IGenericRepository<BookingDish> bookingDishRepository,
        IGenericRepository<RestaurantBooking> restaurantBookingRepository,
        IGenericRepository<Dish> dishRepository)
    {
        _bookingDishRepository = bookingDishRepository;
        _restaurantBookingRepository = restaurantBookingRepository;
        _dishRepository = dishRepository;
    }

    public async Task<Result> Handle(UpdateBookingDishesCommand request, CancellationToken cancellationToken)
    {
        var booking = await _restaurantBookingRepository.GetByIdAsync(request.BookingId);
        if (booking is null)
        {
            return Result.Failure(DomainErrors.RestaurantBooking.NotFound(request.BookingId));
        }

        foreach (var dishEntry in request.Request.DishesIdsWithQuantities)
        {
            var dishId = dishEntry.Key;
            var quantity = dishEntry.Value;

            if (await _dishRepository.GetByIdAsync(dishId) is null)
            {
                return Result.Failure(DomainErrors.Dish.NotFound(dishId));
            }

            var existingBookingDish = await _bookingDishRepository
                .Where(bd => bd.RestaurantBookingId == request.BookingId && bd.DishId == dishId)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingBookingDish != null)
            {
                if (quantity > 0)
                {
                    existingBookingDish.Quantity = quantity;
                }
                else
                {
                    await _bookingDishRepository.RemoveAsync(existingBookingDish.Id);
                }
            }
            else if (quantity > 0)
            {
                var newBookingDish = new BookingDish
                {
                    Id = Guid.NewGuid(),
                    RestaurantBookingId = request.BookingId,
                    DishId = dishId,
                    Quantity = quantity
                };
                await _bookingDishRepository.AddAsync(newBookingDish);
            }
        }

        await _bookingDishRepository.SaveChangesAsync();
        return Result.Success();
    }
}