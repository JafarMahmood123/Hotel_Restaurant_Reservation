using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;

public class AddRestaurantBookingCommandHandler : ICommandHandler<AddRestaurantBookingCommand, RestaurantBooking?>
{
    private readonly IGenericRepository<RestaurantBooking> restaurantBookingRepository;
    private readonly IGenericRepository<BookingDish> bookingDishesRepository;

    public AddRestaurantBookingCommandHandler(IGenericRepository<RestaurantBooking> restaurantBookingRepository,
        IGenericRepository<BookingDish> bookingDishesRepository)
    {
        this.restaurantBookingRepository = restaurantBookingRepository;
        this.bookingDishesRepository = bookingDishesRepository;
    }

    public async Task<RestaurantBooking?> Handle(AddRestaurantBookingCommand request, CancellationToken cancellationToken)
    {
        var restaurantBooking = request.RestaurantBooking;
        restaurantBooking.Id = Guid.NewGuid();

        var existingBookingAtTheRecieveTime = await restaurantBookingRepository.GetFirstOrDefaultAsync(
            x => x.TableNumber == restaurantBooking.TableNumber &&
            (restaurantBooking.ReceiveDateTime >= x.ReceiveDateTime && restaurantBooking.ReceiveDateTime <= x.EndBookingDateTime.AddMinutes(10)));

        if (existingBookingAtTheRecieveTime is not null)
            return null;
        else
        {
            restaurantBooking = await restaurantBookingRepository.AddAsync(restaurantBooking);
            await restaurantBookingRepository.SaveChangesAsync();

            foreach (var bookingDish in request.RestaurantBooking.BookingDishes)
            {
                bookingDish.Id = Guid.NewGuid();
                bookingDish.RestaurantBookingId = restaurantBooking.Id;

                await bookingDishesRepository.AddAsync(bookingDish);
                await bookingDishesRepository.SaveChangesAsync();
            }
        }


        return restaurantBooking;
    }
}
