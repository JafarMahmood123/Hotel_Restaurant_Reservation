namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.AddBookingDishes;

public class AddBookingDishesRequest
{
    public Dictionary<Guid, int> dishesIdsWithQuantities { set; get; }
}
