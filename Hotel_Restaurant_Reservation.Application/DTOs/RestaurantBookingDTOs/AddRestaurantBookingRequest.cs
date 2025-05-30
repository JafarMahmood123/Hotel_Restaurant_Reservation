using Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.AddBookingDishes;

namespace Hotel_Restaurant_Reservation.Application.DTOs.RestaurantBookingDTOs;

public class AddRestaurantBookingRequest
{
    // Key Properties

    public DateTime BookingDateTime { get; set; }

    public DateTime ReceiveDateTime { get; set; }

    public DateTime EndBookingDateTime { get; set; }

    public int NumberOfPeople { get; set; }

    public int TableNumber { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid CustomerId { get; set; }

    public IEnumerable<AddBookingDishesRequest> AddBookingDishRequest { get; set; }
}
