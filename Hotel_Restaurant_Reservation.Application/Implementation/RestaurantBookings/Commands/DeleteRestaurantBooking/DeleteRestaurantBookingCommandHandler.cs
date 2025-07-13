using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.DeleteRestaurantBooking;

public class DeleteRestaurantBookingCommandHandler : ICommandHandler<DeleteRestaurantBookingCommand, Result>
{
    private readonly IGenericRepository<RestaurantBooking> _restaurantBookingRepository;

    public DeleteRestaurantBookingCommandHandler(IGenericRepository<RestaurantBooking> restaurantBookingRepository)
    {
        _restaurantBookingRepository = restaurantBookingRepository;
    }

    public async Task<Result> Handle(DeleteRestaurantBookingCommand request, CancellationToken cancellationToken)
    {
        var restaurantBooking = await _restaurantBookingRepository.GetByIdAsync(request.Id);

        if (restaurantBooking is null)
        {
            return Result.Failure(DomainErrors.RestaurantBooking.NotFound(request.Id));
        }

        await _restaurantBookingRepository.RemoveAsync(request.Id);
        await _restaurantBookingRepository.SaveChangesAsync();

        return Result.Success();
    }
}