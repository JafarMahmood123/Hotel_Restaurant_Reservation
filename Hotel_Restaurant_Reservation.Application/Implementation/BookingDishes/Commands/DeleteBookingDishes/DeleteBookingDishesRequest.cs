namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.DeleteBookingDishes;

public class DeleteBookingDishesRequest
{
    public IEnumerable<Guid> DishIds { get; set; }
}