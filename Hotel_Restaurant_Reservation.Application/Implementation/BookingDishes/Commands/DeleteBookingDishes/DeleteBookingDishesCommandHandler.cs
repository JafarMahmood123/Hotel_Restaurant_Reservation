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

        if (DateTime.UtcNow >= booking.ReceiveDateTime.AddMinutes(-15))
        {
            return Result.Failure(DomainErrors.RestaurantBooking.DeletionNotAllowed(booking.ReceiveDateTime));
        }

        var bookingDishes = await _bookingDishRepository
            .Where(bd => bd.RestaurantBookingId == request.BookingId)
            .ToListAsync(cancellationToken);

        var foundDishesToRemove = bookingDishes
            .Where(bd => request.Request.DishIds.Contains(bd.DishId))
            .ToList();

        var foundDishIds = foundDishesToRemove.Select(d => d.DishId);
        var notFoundDishIds = request.Request.DishIds.Except(foundDishIds).ToList();

        if (notFoundDishIds.Any())
        {
            return Result.Failure(DomainErrors.BookingDishes.SomeNotFound(notFoundDishIds));
        }

        if (!foundDishesToRemove.Any())
        {
            return Result.Failure(DomainErrors.BookingDishes.NotFound);
        }

        _bookingDishRepository.RemoveRange(foundDishesToRemove);
        await _bookingDishRepository.SaveChangesAsync();

        return Result.Success();
    }
}