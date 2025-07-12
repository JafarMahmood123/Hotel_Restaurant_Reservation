namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Queries;

public class BookingDishResponse
{
    public Guid Id { get; set; }

    public Guid RestaurantBookingId { get; set; }

    public Guid DishId { get; set; }

    public int Quantity { get; set; }
}
