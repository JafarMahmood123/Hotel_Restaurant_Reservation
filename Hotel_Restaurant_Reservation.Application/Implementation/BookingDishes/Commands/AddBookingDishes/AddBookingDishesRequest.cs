namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.AddBookingDishes;

public class AddBookingDishesRequest
{
    public Dictionary<Guid, int> dishesIdsWithQuantities { set; get; }
}
