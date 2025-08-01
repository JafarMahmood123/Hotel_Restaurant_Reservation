﻿namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class EventRegistration
{
    // Key Properties

    public Guid Id { get; set; }

    public DateTime RegistrationDateTime { get; set; }

    public int NumberOfPeople { get; set; }

    // Foreign Keys

    public Guid UserId { get; set; }

    public Guid EventId { get; set; }

    // Navigation Properties

    public virtual User User { get; set; }

    public virtual Event Event { get; set; }

    public virtual EventRegistrationPayment EventRegistrationPayment { get; set; } 
}
