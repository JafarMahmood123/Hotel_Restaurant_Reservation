namespace Hotel_Restaurant_Reservation.Application.DTOs.BookingDishDTOs;

public class AddBookingDishRequest
{

    public Guid DishId { get; set; }

    public int Quantity { get; set; }
}
