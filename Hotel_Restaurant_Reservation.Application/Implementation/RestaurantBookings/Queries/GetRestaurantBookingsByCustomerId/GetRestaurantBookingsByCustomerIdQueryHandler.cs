using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries.GetRestaurantBookingsByCustomerId;

public class GetRestaurantBookingsByCustomerIdQueryHandler : IQueryHandler<GetRestaurantBookingsByCustomerIdQuery,
    Result<IEnumerable<RestaurantBookingResponse>>>
{
    private readonly IGenericRepository<RestaurantBooking> _restaurantBookingRepository;
    private readonly IMapper _mapper;

    public GetRestaurantBookingsByCustomerIdQueryHandler(IGenericRepository<RestaurantBooking> restaurantBookingRepository,
        IMapper mapper)
    {
        this._restaurantBookingRepository = restaurantBookingRepository;
        this._mapper = mapper;
    }

    public async Task<Result<IEnumerable<RestaurantBookingResponse>>> Handle(GetRestaurantBookingsByCustomerIdQuery request,
        CancellationToken cancellationToken)
    {
        var customerId = request.CustomerId;

        var bookings = await _restaurantBookingRepository.Where(x => x.CustomerId == customerId)
            .Include(x=>x.BookingDishes)
            .ToListAsync();

        var bookingRespnses = _mapper.Map<IEnumerable<RestaurantBookingResponse>>(bookings);

        return Result.Success(bookingRespnses);
    }
}
