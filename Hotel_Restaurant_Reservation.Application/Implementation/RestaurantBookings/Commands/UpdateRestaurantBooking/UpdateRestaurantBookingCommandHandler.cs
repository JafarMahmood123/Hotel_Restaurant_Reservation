using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.UpdateRestaurantBooking;

public class UpdateRestaurantBookingCommandHandler : ICommandHandler<UpdateRestaurantBookingCommand, Result<RestaurantBookingResponse>>
{
    private readonly IGenericRepository<RestaurantBooking> _restaurantBookingRepository;
    private readonly IMapper _mapper;

    public UpdateRestaurantBookingCommandHandler(IGenericRepository<RestaurantBooking> restaurantBookingRepository, IMapper mapper)
    {
        _restaurantBookingRepository = restaurantBookingRepository;
        _mapper = mapper;
    }

    public async Task<Result<RestaurantBookingResponse>> Handle(UpdateRestaurantBookingCommand request, CancellationToken cancellationToken)
    {
        var restaurantBooking = await _restaurantBookingRepository.GetByIdAsync(request.Id);

        if (restaurantBooking is null)
        {
            return Result.Failure<RestaurantBookingResponse>(DomainErrors.RestaurantBooking.NotFound(request.Id));
        }

        _mapper.Map(request.UpdateRestaurantBookingRequest, restaurantBooking);

        await _restaurantBookingRepository.UpdateAsync(request.Id, restaurantBooking);
        await _restaurantBookingRepository.SaveChangesAsync();

        var restaurantBookingResponse = _mapper.Map<RestaurantBookingResponse>(restaurantBooking);

        return Result.Success(restaurantBookingResponse);
    }
}