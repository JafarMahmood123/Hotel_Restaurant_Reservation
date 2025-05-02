using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.DTOs.RestaurantBookingDTOs;

public class RestaurantBookingResponse
{
    // Key Properties
    public Guid Id { get; set; }

    public DateTime BookingDateTime { get; set; }

    public DateTime ReceiveDateTime { get; set; }

    public DateTime EndBookingDateTime { get; set; }

    public int NumberOfPeople { get; set; }

    public int TableNumber { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid CustomerId { get; set; }

    public ICollection<BookingDish> BookingDishes { get; set; }
}
