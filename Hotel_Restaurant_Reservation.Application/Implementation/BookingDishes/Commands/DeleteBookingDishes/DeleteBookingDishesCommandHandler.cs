using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.DeleteBookingDishes;

public class DeleteBookingDishesCommandHandler : ICommandHandler<DeleteBookingDishesCommand, Result>
{
    private readonly IGenericRepository<BookingDish> _bookingDishRepository;
    private readonly IGenericRepository<RestaurantBooking> _restaurantBookingRepository;

    public DeleteBookingDishesCommandHandler(IGenericRepository<BookingDish> bookingDishRepository, IGenericRepository<RestaurantBooking> restaurantBookingRepository)
    {
        _bookingDishRepository = bookingDishRepository;
        _restaurantBookingRepository = restaurantBookingRepository;
    }

    public async Task<Result> Handle(DeleteBookingDishesCommand request, CancellationToken cancellationToken)
    {
        var booking = await _restaurantBookingRepository.GetByIdAsync(request.BookingId);
        if (booking is null)
        {
            return Result.Failure(DomainErrors.RestaurantBooking.NotFound(request.BookingId));
        }

        var dishesToRemove = await _bookingDishRepository
            .Where(bd => bd.RestaurantBookingId == request.BookingId && request.Request.DishIds.Contains(bd.DishId))
            .ToListAsync(cancellationToken);

        if (!dishesToRemove.Any())
        {
            return Result.Failure(DomainErrors.BookingDishes.NotFound);
        }

        _bookingDishRepository.RemoveRange(dishesToRemove);
        await _bookingDishRepository.SaveChangesAsync();

        return Result.Success();
    }
}