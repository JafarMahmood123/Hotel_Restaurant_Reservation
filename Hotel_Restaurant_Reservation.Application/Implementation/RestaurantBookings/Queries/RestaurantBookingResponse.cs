using Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries;

public class RestaurantBookingResponse
{
    // Key Properties
    public Guid Id { get; set; }

    public DateTime BookingDateTime { get; set; }

    public DateTime ReceiveDateTime { get; set; }

    public TimeOnly BookingDurationTime { get; set; }

    public int NumberOfPeople { get; set; }

    public int TableNumber { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid CustomerId { get; set; }

    public ICollection<BookingDishResponse> BookingDishes { get; set; }
}
