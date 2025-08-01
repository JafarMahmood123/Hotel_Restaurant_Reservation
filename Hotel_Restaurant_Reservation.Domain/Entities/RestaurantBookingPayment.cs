﻿using Hotel_Restaurant_Reservation.Domain.Enums;

namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantBookingPayment
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public Guid? CurrencyTypeId { get; set; }
    public PaymentStatus Status { get; set; }
    public Guid RestaurantBookingId { get; set; }
    public virtual RestaurantBooking RestaurantBooking { get; set; }
}