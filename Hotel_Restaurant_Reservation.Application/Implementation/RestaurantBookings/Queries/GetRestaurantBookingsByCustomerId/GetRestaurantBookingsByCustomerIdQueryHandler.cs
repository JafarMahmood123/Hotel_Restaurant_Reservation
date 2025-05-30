using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries.GetRestaurantBookingsByCustomerId;

public class GetRestaurantBookingsByCustomerIdQueryHandler : IQueryHandler<GetRestaurantBookingsByCustomerIdQuery,
    IEnumerable<RestaurantBooking>?>
{
    private readonly IGenericRepository<RestaurantBooking> restaurantBookingRepository;

    public GetRestaurantBookingsByCustomerIdQueryHandler(IGenericRepository<RestaurantBooking> restaurantBookingRepository)
    {
        this.restaurantBookingRepository = restaurantBookingRepository;
    }

    public async Task<IEnumerable<RestaurantBooking>?> Handle(GetRestaurantBookingsByCustomerIdQuery request,
        CancellationToken cancellationToken)
    {
        var customerId = request.CustomerId;

        var bookings = await restaurantBookingRepository.Where(x => x.CustomerId == customerId)
            .Include(x=>x.BookingDishes)
            .ToListAsync();


        return bookings;
    }
}
