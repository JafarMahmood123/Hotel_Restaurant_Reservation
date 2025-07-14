using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries.GetAllRestaurantBookingsByRestaurantId;

public class GetAllRestaurantBookingsByRestaurantIdQueryHandler : IQueryHandler<GetAllRestaurantBookingsByRestaurantIdQuery, Result<IEnumerable<RestaurantBookingResponse>>>
{
    private readonly IGenericRepository<RestaurantBooking> _restaurantBookingRepository;
    private readonly IMapper _mapper;

    public GetAllRestaurantBookingsByRestaurantIdQueryHandler(IGenericRepository<RestaurantBooking> restaurantBookingRepository, IMapper mapper)
    {
        _restaurantBookingRepository = restaurantBookingRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<RestaurantBookingResponse>>> Handle(GetAllRestaurantBookingsByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var bookings = await _restaurantBookingRepository
            .Where(x => x.RestaurantId == request.RestaurantId)
            .Include(x => x.BookingDishes)
            .ToListAsync(cancellationToken);

        var bookingResponses = _mapper.Map<IEnumerable<RestaurantBookingResponse>>(bookings);

        return Result.Success(bookingResponses);
    }
}