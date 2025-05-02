using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.AddBookingDishes;

public class AddBookingDishCommandHandler : ICommandHandler<AddBookingDishCommand, BookingDish>
{
    private readonly IGenericRepository<BookingDish> bookingDishRepository;

    public AddBookingDishCommandHandler(IGenericRepository<BookingDish> bookingDishRepository)
    {
        this.bookingDishRepository = bookingDishRepository;
    }

    public async Task<BookingDish> Handle(AddBookingDishCommand request, CancellationToken cancellationToken)
    {
        var bookingDish = request.BookingDish;

        bookingDish.Id = Guid.NewGuid();

        bookingDish.RestaurantBookingId = request.RestaurantBooking.Id;

        await bookingDishRepository.AddAsync(bookingDish);

        await bookingDishRepository.SaveChangesAsync();

        return bookingDish;
    }
}
