namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.UpdateBookingDishes;

public class UpdateBookingDishesRequest
{
    public Dictionary<Guid, int> DishesIdsWithQuantities { get; set; }
}