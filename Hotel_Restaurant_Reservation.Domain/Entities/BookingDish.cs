namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class BookingDish
{
    public Guid Id { get; set; }

    public Guid RestaurantBookingId { get; set; }

    public Guid DishId { get; set; }

    public int Quantity { get; set; }

    public virtual Dish Dish { get; set; }

    public virtual RestaurantBooking RestaurantBooking { get; set; }
}
